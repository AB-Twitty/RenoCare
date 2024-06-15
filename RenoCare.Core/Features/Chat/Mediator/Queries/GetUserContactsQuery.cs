using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Features.Chat.Dtos;
using RenoCare.Domain;
using RenoCare.Domain.Chat;
using System.Collections.Generic;
using System.Linq;
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

        #endregion

        #region Ctor

        public GetUserContactsQueryRequestHandler(IRepository<ChatMessage> msgRepo,
            IRepository<MedicationRequest> medRequestsRepo, IHttpContextAccessor ctxAccessor,
            IRepository<Patient> patientRepo, IRepository<DialysisUnit> dialysisUnitRepo)
        {
            _msgRepo = msgRepo;
            _medRequestsRepo = medRequestsRepo;
            _ctxAccessor = ctxAccessor;
            _patientRepo = patientRepo;
            _dialysisUnitRepo = dialysisUnitRepo;
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
                .Where(c => c.Type == "sub").FirstOrDefault()?.Value;

            IList<ContactDto> contacts = new List<ContactDto>();

            var msgContacts = await _msgRepo.ApplyQueryAsync(async qry =>
            {
                var asSender = await qry.Where(x => x.SenderId == curr_user)
                    .Select(x => new ContactDto
                    {
                        Name = x.Receiver.FirstName + " " + x.Receiver.LastName,
                        UserId = x.ReceiverId
                    }).Distinct().ToListAsync();


                var asReceiver = await qry.Where(x => x.ReceiverId == curr_user)
                    .Select(x => new ContactDto
                    {
                        Name = x.Sender.FirstName + " " + x.Sender.LastName,
                        UserId = x.SenderId
                    }).Distinct().ToListAsync();

                return asSender.Union(asReceiver);
            });

            contacts = contacts.Union(msgContacts).ToList();

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
                            Name = x.Patient.User.FirstName + " " + x.Patient.User.LastName,
                            UserId = x.Patient.User.Id
                        }).Distinct().ToListAsync();
                });

                contacts = contacts.Union(medRequestsContacts).Distinct().ToList();

                foreach (var contact in contacts)
                {
                    int? contactId = await _patientRepo.ApplyQueryAsync(async qry =>
                        await qry.Where(p => p.UserId == contact.UserId).Select(p => p.Id).FirstOrDefaultAsync());

                    if (contactId == null)
                    {
                        contacts.Remove(contact);
                        continue;
                    }

                    contact.ContactId = contactId.Value;
                }
            }
            else if (_ctxAccessor.HttpContext.User.IsInRole("Patient"))
            {
                contacts = contacts.Distinct().ToList();

                foreach (var contact in contacts)
                {
                    int? contactId = await _dialysisUnitRepo.ApplyQueryAsync(async qry =>
                        await qry.Where(p => p.UserId == contact.UserId).Select(p => p.Id).FirstOrDefaultAsync());

                    if (contactId == null)
                    {
                        contacts.Remove(contact);
                        continue;
                    }

                    contact.ContactId = contactId.Value;
                }
            }

            return Success(contacts);
        }

        #endregion
    }

}
