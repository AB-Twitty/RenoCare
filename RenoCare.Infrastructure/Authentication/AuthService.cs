using Microsoft.AspNetCore.Identity;
using RenoCare.Core.Features.Authentication.Contracts;
using RenoCare.Core.Features.Authentication.Contracts.Models;
using RenoCare.Domain.Identity;
using RenoCare.Domain.MetaData;
using RenoCare.Infrastructure.Authentication.Contracts;
using System;
using System.Threading.Tasks;

namespace RenoCare.Infrastructure.Authentication
{
    /// <summary>
    /// Represents the authentiction service.
    /// </summary>
    public class AuthService : IAuthService
    {
        #region Fields

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenProvider _tokenProvider;

        #endregion

        #region Ctor
        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenProvider tokenProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenProvider = tokenProvider;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Authenticate the user.
        /// </summary>
        /// <param name="request">An auth request the contains properties needed for the authentication process.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task contains the auth response.
        /// </returns>
        public async Task<AuthResponse> AuthenticateAsync(AuthRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Email);

            if (user == null)
                throw new Exception(Transcriptor.Identity.InvalidAuthentication);

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, false);

            if (!result.Succeeded)
                throw new Exception(Transcriptor.Identity.InvalidAuthentication);

            var response = new AuthResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AccessToken = await _tokenProvider.GenerateTokenAsync(user)
            };

            return response;
        }

        /// <summary>
        /// Generate an email confirmation token.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the email confirmation token.
        /// </returns>
        public async Task<string> GenerateEmailConfirmationTokenAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new ArgumentNullException(string.Format(Transcriptor.Identity.UserNotFound, userId));

            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        /// <summary>
        /// Confirm user email.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="token">Email confirmation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains a value indicating whether the confirmation succeeded.
        /// </returns>
        public async Task<bool> ConfirmEmailAsync(string userId, string token)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            if (token == null)
                throw new ArgumentNullException(nameof(token));

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new ArgumentNullException(string.Format(Transcriptor.Identity.UserNotFound, userId));

            var result = await _userManager.ConfirmEmailAsync(user, token);

            return result.Succeeded;
        }

        /// <summary>
        /// Get user details by his id.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the details of the user.
        /// </returns>
        public async Task<AppUser> GetUserByIdAsync(string userId)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            return await _userManager.FindByIdAsync(userId);
        }


        #endregion
    }
}