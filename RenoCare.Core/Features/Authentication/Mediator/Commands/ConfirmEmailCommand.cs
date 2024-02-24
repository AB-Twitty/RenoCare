using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using RenoCare.Core.Base;
using RenoCare.Core.Features.Authentication.Contracts;
using RenoCare.Domain.MetaData;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.Authentication.Mediator.Commands
{
    /// <summary>
    /// Represents an email confirmation request with specidfied properties with a corresponding response.
    /// </summary>
    public class ConfirmEmailCommandRequest : IRequest<ApiResponse<string>>
    {
        public string UserId { get; set; }
        public string EncodedToken { get; set; }
    }

    /// <summary>
    /// Represents a handler for confirming email request.
    /// </summary>
    public class ConfirmEmailCommandRequestHandler : ResponseHandler, IRequestHandler<ConfirmEmailCommandRequest, ApiResponse<string>>
    {
        #region Fields

        private readonly IAuthService _authService;

        #endregion

        #region Ctor

        public ConfirmEmailCommandRequestHandler(IAuthService authService)
        {
            _authService = authService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles an email confirmation request.
        /// </summary>
        /// <param name="request">Email confirming request.</param>
        /// <param name="cancellationToken">Represents a notification to cancel the request.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the authentication response.
        /// </returns>
        public async Task<ApiResponse<string>> Handle(ConfirmEmailCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.EncodedToken == null)
                throw new ArgumentNullException(nameof(request.EncodedToken));

            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.EncodedToken));

            if (await _authService.ConfirmEmailAsync(request.UserId, decodedToken))
            {
                return Success(Boolean.TrueString, Transcriptor.Identity.EmailConfirmedSuccess);
            }

            return Success(Boolean.FalseString, Transcriptor.Identity.EmailSendingFailure);
        }

        #endregion
    }
}
