using MediatR;
using RenoCare.Core.Base;
using RenoCare.Core.Features.Authentication.Contracts;
using RenoCare.Core.Features.Authentication.Contracts.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.Authentication.Mediator.Commands
{
    /// <summary>
    /// Represents a login request with specidfied properties with a corresponding response.
    /// </summary>
    public class LoginCommandRequest : AuthRequest, IRequest<ApiResponse<AuthResponse>>
    {
        public LoginCommandRequest(AuthRequest request)
        {
            Email = request.Email;
            Password = request.Password;
            RememberMe = request.RememberMe;
        }
    }

    /// <summary>
    /// Represents a handler for the login request.
    /// </summary>
    public class LoginCommandRequestHandler : ResponseHandler, IRequestHandler<LoginCommandRequest, ApiResponse<AuthResponse>>
    {
        #region Fields

        private readonly IAuthService _authService;

        #endregion

        #region Ctor

        public LoginCommandRequestHandler(IAuthService authService)
        {
            _authService = authService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the login request.
        /// </summary>
        /// <param name="request">Login request.</param>
        /// <param name="cancellationToken">Represents a notification to cancel the request.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task contains the authentication response.
        /// </returns>
        public async Task<ApiResponse<AuthResponse>> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            var response = await _authService.AuthenticateAsync(request);

            return Success(response, "Logged in Successfully");
        }

        #endregion
    }
}
