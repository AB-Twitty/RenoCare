using RenoCare.Core.Features.Authentication.Contracts.Models;
using RenoCare.Domain.Identity;
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

        /// <summary>
        /// Generate an email confirmation token.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the email confirmation token.
        /// </returns>
        public Task<string> GenerateEmailConfirmationTokenAsync(string userId);

        /// <summary>
        /// Confirm user email.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="token">Email confirmation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains a value indicating whether the confirmation succeeded.
        /// </returns>
        public Task<bool> ConfirmEmailAsync(string userId, string token);

        /// <summary>
        /// Get user details by his id.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the details of the user.
        /// </returns>
        public Task<AppUser> GetUserByIdAsync(string userId);
    }
}
