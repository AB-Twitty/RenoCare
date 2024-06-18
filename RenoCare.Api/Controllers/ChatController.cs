using MediatR;
using Microsoft.AspNetCore.Mvc;
using RenoCare.Core.Base;
using RenoCare.Core.Features.Chat.Dtos;
using RenoCare.Core.Features.Chat.Mediator.Commands;
using RenoCare.Core.Features.Chat.Mediator.Queries;
using RenoCare.Core.Helpers;
using RenoCare.Domain.Chat;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenoCare.Api.Controllers
{
    public class ChatController : BaseController
    {
        private readonly IMediator _mediator;

        public ChatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/chat/contacts")]
        public async Task<ActionResult<ApiResponse<IList<ContactDto>>>> GetUserContactsAsync() =>
            ApiResult(await _mediator.Send(new GetUserContactsQueryRequest()));

        [HttpGet("/chat/messages/{userId}")]
        public async Task<IPagedList<ChatMessage>> GetCurrentUserChatWithAsync(
            [FromRoute] string userId, [FromQuery] int page = 1, int pageSize = 20)
        {
            return await _mediator.Send(new GetCurrentUserChatWithQueryRequest
            {
                UserId = userId,
                Page = page,
                PageSize = pageSize
            });
        }

        [HttpPost("/chat/upload")]
        public async Task<ActionResult<ApiResponse<string>>> UploadChatFileAsync([FromForm] UploadChatFileCommandRequest req) =>
            ApiResult(await _mediator.Send(req));
    }
}
