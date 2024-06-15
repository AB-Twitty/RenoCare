using MediatR;
using Microsoft.AspNetCore.Http;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Helpers;
using RenoCare.Domain.Chat;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.Chat.Mediator.Queries
{
    public class GetCurrentUserChatWithQueryRequest : IRequest<IPagedList<ChatMessage>>
    {
        public string UserId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class GetCurrentUserChatWithQueryRequestHandler : ResponseHandler,
        IRequestHandler<GetCurrentUserChatWithQueryRequest, IPagedList<ChatMessage>>
    {
        private readonly IRepository<ChatMessage> _msgRepo;
        private readonly IHttpContextAccessor _ctxAccessor;

        public GetCurrentUserChatWithQueryRequestHandler(IRepository<ChatMessage> msgRepo, IHttpContextAccessor ctxAccessor)
        {
            _msgRepo = msgRepo;
            _ctxAccessor = ctxAccessor;
        }

        public async Task<IPagedList<ChatMessage>> Handle(GetCurrentUserChatWithQueryRequest request, CancellationToken cancellationToken)
        {
            var curr_user = _ctxAccessor.HttpContext.User.Claims
                .Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;

            return await _msgRepo.GetAllPagedAsync(qry =>
                qry.Where(x => x.SenderId == curr_user || x.ReceiverId == curr_user)
                .OrderByDescending(x => x.SendingTime), request.Page, request.PageSize, 1, false);
        }
    }
}
