using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Files;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Hubs;
using RenoCare.Domain.Chat;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.Chat.Mediator.Commands
{
    /// <summary>
    /// Represents a command to upload file from chat.
    /// </summary>
    public class UploadChatFileCommandRequest : IRequest<ApiResponse<string>>
    {
        public IFormFile File { get; set; }
        //public string FileName { get; set; }
        public string ReceiverId { get; set; }
    }

    // <summary>
    /// Represents a handler for the command to upload file from chat.
    /// </summary>
    public class UploadChatFileCommandRequestHandler : ResponseHandler,
        IRequestHandler<UploadChatFileCommandRequest, ApiResponse<string>>
    {
        #region Fields

        private readonly IRepository<ChatMessage> _msgRepo;
        private readonly IWebHostEnvironment _webEnv;
        private readonly IHubContext<ChatHub> _chatHub;
        private readonly IHttpContextAccessor _ctxAccessor;
        private readonly IFileUpload _fileUpload;

        #endregion

        public UploadChatFileCommandRequestHandler(IRepository<ChatMessage> msgRepo
            , IWebHostEnvironment webEnv, IHubContext<ChatHub> chatHub
            , IHttpContextAccessor ctxAccessor, IFileUpload fileUpload)
        {
            _msgRepo = msgRepo;
            _webEnv = webEnv;
            _chatHub = chatHub;
            _ctxAccessor = ctxAccessor;
            _fileUpload = fileUpload;
        }

        #region Ctor

        #endregion

        #region Methods

        /// <summary>
        /// Handles the command to upload file from chat.
        /// </summary>
        /// <param name="request">Mediator request</param>
        /// <param name="cancellationToken">Represents a notification to cancel the request.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains a bool to indicate if the request is successfull or not.
        /// </returns>
        public async Task<ApiResponse<string>> Handle(UploadChatFileCommandRequest request, CancellationToken cancellationToken)
        {
            var curr_user = _ctxAccessor.HttpContext.User.Claims
                .Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;

            if (request.File == null)
                return BadRequest<string>("No file was provided.");


            var file_path = await _fileUpload.UploadFileAsync(request.File, FileDir.Uploads);

            var file_msg = new ChatMessage
            {
                SenderId = curr_user,
                ReceiverId = request.ReceiverId,
                Status = 1,
                Message = request.File.FileName,
                FileLink = file_path,
                IsFile = true,
                SendingTime = DateTime.Now
            };

            await _msgRepo.InsertAsync(file_msg);
            await _msgRepo.SaveAsync();

            await _chatHub.Clients.Users(curr_user, request.ReceiverId).SendAsync("ReceiveMessage", file_msg);

            return Success(file_path, "Uploaded");
        }

        #endregion
    }
}
