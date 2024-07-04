using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Features.Authentication.Contracts;
using RenoCare.Core.Features.Authentication.Contracts.Models;
using RenoCare.Domain;
using RenoCare.Domain.Identity;
using RenoCare.Domain.MetaData;
using RenoCare.Infrastructure.Authentication.Contracts;
using System;
using System.Collections.Generic;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenProvider _tokenProvider;
        private readonly IRepository<MedicationRequest> _medReqRepo;
        private readonly IRepository<Report> _reportRepo;

        #endregion

        #region Ctor
        public AuthService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, ITokenProvider tokenProvider,
            RoleManager<IdentityRole> roleManager, IRepository<MedicationRequest> medReqRepo, IRepository<Report> reportRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenProvider = tokenProvider;
            _roleManager = roleManager;
            _medReqRepo = medReqRepo;
            _reportRepo = reportRepo;
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

        /// <summary>
        /// Create a new account and sends an OTP email.
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="role">user role</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the OTP token and the new user.
        /// </returns>
        public async Task<(string, AppUser)> CreateAccountWithOTPAsync(string email, string role)
        {
            var result = await _userManager.CreateAsync(new AppUser { Email = email, UserName = email });

            if (!result.Succeeded)
            {
                throw new Exception();
            }

            var user = await _userManager.FindByNameAsync(email);

            await _userManager.AddToRoleAsync(user, role);

            var code = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider);

            return (code, user);
        }

        /// <summary>
        /// Checks if there is a user with a given email.
        /// </summary>
        /// <param name="email">the email to check</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains a value indicating where there is a user with this email or not.
        /// </returns>
        public async Task<bool> IsUserWithEmailExistsAsync(string email)
        {
            return await _userManager.FindByNameAsync(email) != null;
        }

        /// <summary>
        /// validate the otp.
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="otp">OTP</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains a value whether the otp is valid or not.
        /// </returns>
        public async Task<bool> ValidateOtpAsync(string email, string otp)
        {
            var user = await _userManager.FindByNameAsync(email);

            if (user == null)
                return false;

            return await _userManager.VerifyTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider, otp);
        }

        /// <summary>
        /// Generate a password reset token.
        /// </summary>
        /// <param name="email">user email</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the reset password token.
        /// </returns>
        public async Task<string> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await _userManager.FindByNameAsync(email);

            if (user == null)
                throw new ArgumentNullException(string.Format(Transcriptor.Identity.UserNotFound, email));

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        /// <summary>
        /// Checks whether the user already has a password set or not.
        /// </summary>
        /// <param name="email">the user email</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains a value indicating whether the user has a password or not.
        /// </returns>
        public async Task<bool> HasPasswordAsync(string email)
        {
            var user = await _userManager.FindByNameAsync(email);

            if (user == null)
                throw new ArgumentNullException(string.Format(Transcriptor.Identity.UserNotFound, email));

            return await _userManager.HasPasswordAsync(user);
        }

        /// <summary>
        /// Add first time password to a given user.
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="password">user first time password</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains a value indicating the succession of the request with any faced errors.
        /// </returns>
        public async Task<(bool, IEnumerable<ValidationFailure>)> AddPasswordAsync(string email, string password)
        {
            var user = await _userManager.FindByNameAsync(email);

            if (user == null)
                throw new ArgumentNullException(string.Format(Transcriptor.Identity.UserNotFound, email));

            var result = await _userManager.AddPasswordAsync(user, password);

            var errors = new List<ValidationFailure>();
            foreach (var error in result.Errors)
                errors.Add(new ValidationFailure("Password", error.Description));

            return (result.Succeeded, errors);
        }

        /// <summary>
        /// Creates a new user and add it to a given role
        /// </summary>
        /// <param name="user">the new user</param>
        /// <param name="password">the user's password</param>
        /// <param name="role">the user's role</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains a value indicating the succession of the request with any faced errors.
        /// </returns>
        public async Task<(bool, IEnumerable<ValidationFailure>)> AddNewUserAsync(AppUser user, string password, string role)
        {
            if (await IsUserWithEmailExistsAsync(user.Email))
                throw new Exception();

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var errors = new List<ValidationFailure>();
                foreach (var error in result.Errors)
                    errors.Add(new ValidationFailure("Password", error.Description));

                return (result.Succeeded, errors);
            }

            result = await _userManager.AddToRoleAsync(user, role);

            return (result.Succeeded, new List<ValidationFailure>());
        }

        /// <summary>
        /// Update the given user
        /// </summary>
        /// <param name="user">the updated user</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public async Task UpdateUserInfoAsync(AppUser user)
        {
            await _userManager.UpdateAsync(user);
        }

        /// <summary>
        /// Delete a given user
        /// </summary>
        /// <param name="user">the user</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public async Task DeleteUserAsync(AppUser user)
        {
            await _userManager.DeleteAsync(user);
        }

        /// <summary>
        /// Checks if a user is in a given role or not
        /// </summary>
        /// <param name="user">the user</param>
        /// <param name="role">the role to check</param>
        /// <returns>
        /// A value indicating whether the user is in a given role or not
        /// </returns>
        public async Task<bool> IsUserInRole(AppUser user, string role)
        {
            return await _userManager.IsInRoleAsync(user, role);
        }

        /// <summary>
        /// Counte the number of user in a gven role
        /// </summary>
        /// <param name="role"></param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the number of the user in the given role.
        /// </returns>
        public async Task<int> CountInRole(string role)
        {
            return (await _userManager.GetUsersInRoleAsync(role)).Count;
        }

        #endregion
    }
}