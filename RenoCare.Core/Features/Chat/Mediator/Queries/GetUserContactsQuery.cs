using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Features.Chat.Dtos;
using RenoCare.Core.Hubs;
using RenoCare.Domain;
using RenoCare.Domain.Chat;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.Chat.Mediator.Queries
{
    /// <summary>
    /// Represents a query to get the current user contacts.
    /// </summary>
    public class GetUserContactsQueryRequest : IRequest<ApiResponse<IList<ContactDto>>>
    {
    }

    // <summary>
    /// Represents a handler for query to get the current user contacts.
    /// </summary>
    public class GetUserContactsQueryRequestHandler : ResponseHandler,
        IRequestHandler<GetUserContactsQueryRequest, ApiResponse<IList<ContactDto>>>
    {
        #region Fields

        private readonly IRepository<ChatMessage> _msgRepo;
        private readonly IRepository<MedicationRequest> _medRequestsRepo;
        private readonly IRepository<Patient> _patientRepo;
        private readonly IRepository<DialysisUnit> _dialysisUnitRepo;
        private readonly IHttpContextAccessor _ctxAccessor;
        private readonly IHubContext<ChatHub> _chatHub;

        #endregion

        #region Ctor

        public GetUserContactsQueryRequestHandler(IRepository<ChatMessage> msgRepo,
            IRepository<MedicationRequest> medRequestsRepo, IHttpContextAccessor ctxAccessor,
            IRepository<Patient> patientRepo, IRepository<DialysisUnit> dialysisUnitRepo, IHubContext<ChatHub> chatHub)
        {
            _msgRepo = msgRepo;
            _medRequestsRepo = medRequestsRepo;
            _ctxAccessor = ctxAccessor;
            _patientRepo = patientRepo;
            _dialysisUnitRepo = dialysisUnitRepo;
            _chatHub = chatHub;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the request to get the current user contacts.
        /// </summary>
        /// <param name="request">Mediator request</param>
        /// <param name="cancellationToken">Represents a notification to cancel the request.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains a list of the current user contacts.
        /// </returns>
        public async Task<ApiResponse<IList<ContactDto>>> Handle(GetUserContactsQueryRequest request, CancellationToken cancellationToken)
        {
            var curr_user = _ctxAccessor.HttpContext.User.Claims
                .Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;

            var cnotact_comparer = new ContactComparer();

            IList<ContactDto> contacts = new List<ContactDto>();

            var msgContacts = await _msgRepo.ApplyQueryAsync(async qry =>
            {
                var asSender = await qry.Where(x => x.SenderId == curr_user)
                    .Select(x => new ContactDto
                    {
                        UserId = x.ReceiverId
                    }).Distinct().ToListAsync();


                var asReceiver = await qry.Where(x => x.ReceiverId == curr_user)
                    .Select(x => new ContactDto
                    {
                        UserId = x.SenderId
                    }).Distinct().ToListAsync();

                return asSender.Union(asReceiver);
            });

            contacts = contacts.Union(msgContacts).ToList();

            IList<int> contacts_delete = new List<int>();

            if (_ctxAccessor.HttpContext.User.IsInRole("HealthCare"))
            {
                if (!int.TryParse(_ctxAccessor.HttpContext.Items["unitId"].ToString(), out int unitId))
                    return BadRequest<IList<ContactDto>>();

                var medRequestsContacts = await _medRequestsRepo.ApplyQueryAsync(async qry =>
                {
                    var statuses = new string[] { "pending", "completed", "upcoming" };

                    return await qry.Where(x => x.DialysisUnitId == unitId &&
                        statuses.Contains(x.Status.Name.ToLower()))
                        .Select(x => new ContactDto
                        {
                            UserId = x.Patient.User.Id
                        }).Distinct().ToListAsync();
                });

                contacts = contacts.Union(medRequestsContacts).Distinct(cnotact_comparer).ToList();

                foreach (var contact in contacts)
                {
                    var obj = await _patientRepo.ApplyQueryAsync(async qry =>
                        await qry.Where(p => p.UserId == contact.UserId)
                        .Select(p => new
                        {
                            contact_id = p.Id,
                            contact_name = p.User.FirstName + " " + p.User.LastName
                        })
                        .FirstOrDefaultAsync());

                    if (obj == null)
                    {
                        contacts_delete.Add(contacts.IndexOf(contact));
                        continue;
                    }

                    (ChatMessage last_msg, int unread_cnt) = await _msgRepo.ApplyQueryAsync(async qry =>
                    {
                        int cnt = qry.Where(m => m.ReceiverId == curr_user && m.SenderId == contact.UserId && m.Status == 2).Count();

                        ChatMessage last = await qry.Where(x => (x.SenderId == curr_user && x.ReceiverId == contact.UserId)
                                || (x.ReceiverId == curr_user && x.SenderId == contact.UserId))
                             .OrderByDescending(x => x.SendingTime).FirstOrDefaultAsync();

                        return (last, cnt);
                    });

                    contact.LastMsg = last_msg;
                    contact.UnreadMsgCount = unread_cnt;
                    contact.ContactId = obj.contact_id;
                    contact.Name = obj.contact_name;
                }
            }
            else if (_ctxAccessor.HttpContext.User.IsInRole("Patient"))
            {
                contacts = contacts.Distinct(cnotact_comparer).ToList();

                foreach (var contact in contacts)
                {
                    var obj = await _dialysisUnitRepo.ApplyQueryAsync(async qry =>
                        await qry.Where(p => p.UserId == contact.UserId)
                        .Select(p => new { contact_id = p.Id, contact_name = p.Name })
                        .FirstOrDefaultAsync());

                    if (obj == null)
                    {
                        contacts_delete.Add(contacts.IndexOf(contact));
                        continue;
                    }

                    (ChatMessage last_msg, int unread_cnt) = await _msgRepo.ApplyQueryAsync(async qry =>
                    {
                        int cnt = qry.Where(m => m.ReceiverId == curr_user && m.SenderId == contact.UserId && m.Status == 2).Count();

                        ChatMessage last = await qry.Where(x => (x.SenderId == curr_user && x.ReceiverId == contact.UserId)
                                || (x.ReceiverId == curr_user && x.SenderId == contact.UserId))
                             .OrderByDescending(x => x.SendingTime).FirstOrDefaultAsync();

                        return (last, cnt);
                    });

                    contact.LastMsg = last_msg;
                    contact.UnreadMsgCount = unread_cnt;
                    contact.ContactId = obj.contact_id;
                    contact.Name = obj.contact_name;
                }
            }

            var msgs = await _msgRepo.GetAllAsync(qry =>
                        qry.Where(x => x.ReceiverId == curr_user && x.Status == 1));

            foreach (var msg in msgs)
            {
                msg.Status = 2;
                await _msgRepo.UpdateAsync(msg);
                await _msgRepo.SaveAsync();
                await _chatHub.Clients.User(msg.SenderId).SendAsync("MarkedAsReceived", msg);
            }

            foreach (var idx in contacts_delete)
                contacts.RemoveAt(idx);

            return Success(contacts);
        }

        #endregion
    }

}
