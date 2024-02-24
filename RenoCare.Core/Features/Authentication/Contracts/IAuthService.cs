using RenoCare.Core.Features.Authentication.Contracts.Models;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.Authentication.Contracts
{
    /// <summary>
    /// Represents the authentiction service.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Authenticate the user.
        /// </summary>
        /// <param name="request">An auth request the contains properties needed for the authentication process.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the auth response.
        /// </returns>
        public Task<AuthResponse> AuthenticateAsync(AuthRequest request);
    }
}
