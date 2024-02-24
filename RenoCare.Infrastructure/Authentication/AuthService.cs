using Microsoft.AspNetCore.Identity;
using RenoCare.Core.Features.Authentication.Contracts;
using RenoCare.Core.Features.Authentication.Contracts.Models;
using RenoCare.Domain.Identity;
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

        #endregion

        #region Ctor
        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                throw new Exception("Invalid Auth");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, false);

            if (!result.Succeeded)
                throw new Exception("Invalid Auth");

            var response = new AuthResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AccessToken = ""
            };

            return response;
        }
    }
}