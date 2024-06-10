using FluentValidation.Results;
using RenoCare.Core.Features.Authentication.Contracts.Models;
using RenoCare.Domain.Identity;
using System.Collections.Generic;
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

        /// <summary>
        /// Create a new account and sends an OTP email.
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="role">user role</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the OTP token.
        /// </returns>
        public Task<string> CreateAccountWithOTPAsync(string email, string role);

        /// <summary>
        /// Checks if there is a user with a given email.
        /// </summary>
        /// <param name="email">the email to check</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains a value indicating where there is a user with this email or not.
        /// </returns>
        public Task<bool> IsUserWithEmailExistsAsync(string email);

        /// <summary>
        /// validate the otp.
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="otp">OTP</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains a value indicating the otp is valid or not.
        /// </returns>
        public Task<bool> ValidateOtpAsync(string email, string otp);

        /// <summary>
        /// Generate a password reset token.
        /// </summary>
        /// <param name="email">user email</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the reset password token.
        /// </returns>
        public Task<string> GeneratePasswordResetTokenAsync(string email);

        /// <summary>
        /// Checks whether the user already has a password set or not.
        /// </summary>
        /// <param name="email">the user email</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains a value indicating whether the user has a password or not.
        /// </returns>
        public Task<bool> HasPasswordAsync(string email);

        /// <summary>
        /// Add first time password to a given user.
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="password">user first time password</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains a value indicating the succession of the request with any faced errors.
        /// </returns>
        public Task<(bool, IEnumerable<ValidationFailure>)> AddPasswordAsync(string email, string password);
    }
}
