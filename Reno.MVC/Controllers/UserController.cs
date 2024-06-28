using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reno.MVC.Models.User.Auth;
using Reno.MVC.Services.Base;
using Reno.MVC.Services.Base.Contracts;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Reno.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IClient _client;
        private readonly ILocalStorageService _localStorageService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IClient client, ILocalStorageService localStorageService, IHttpContextAccessor httpContextAccessor)
        {
            _client = client;
            _localStorageService = localStorageService;
            _httpContextAccessor = httpContextAccessor;
        }

        private async Task SaveToken(AuthResponse auth, bool rememberMe)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = tokenHandler.ReadJwtToken(auth.AccessToken);

            var claims = tokenContent.Claims.ToList();

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user,
                new AuthenticationProperties
                {
                    IsPersistent = rememberMe,
                    ExpiresUtc = rememberMe ? DateTime.UtcNow.AddDays(7) : DateTime.UtcNow.AddHours(1)
                });

            _localStorageService.SetStorageValue($"{claims.Where(c => c.Type == "sub").FirstOrDefault().Value}_Token",
                auth.AccessToken);
        }


        [HttpGet("Login")]
        public virtual IActionResult LoginAsync(string returnUrl = null)
        {
            _localStorageService.ClearCurrentToken();
            return View();
        }

        [HttpPost("Login")]
        public virtual async Task<IActionResult> LoginAsync(LoginModel login, string returnUrl = null)
        {
            try
            {
                _localStorageService.ClearCurrentToken();

                await _httpContextAccessor.HttpContext.SignOutAsync();

                if (!ModelState.IsValid)
                    return View(login);

                returnUrl ??= Url.Content("~/");
                var result = await _client.LoginAsync(new AuthRequest
                {
                    Email = login.Email,
                    Password = login.Password,
                    RememberMe = login.RememberMe
                });

                if (!result.Succeded)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(login);
                }

                await SaveToken(result.Data, login.RememberMe);

                return LocalRedirect(returnUrl);
            }
            catch (ApiException<ApiResponse<AuthResponse>> ex)
            {
                ModelState.AddModelError("", ex.Result.Message);
                return View(login);
            }
        }

        public virtual async Task<IActionResult> LogoutAsync()
        {
            _localStorageService.ClearCurrentToken();
            await _httpContextAccessor.HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Password/Reset/OTP")]
        public virtual IActionResult SetPasswordWithOtpAsync()
        {
            _localStorageService.ClearCurrentToken();
            return View();
        }

        [HttpPost("Password/Reset/OTP")]
        public virtual async Task<IActionResult> SetPasswordWithOtpAsync(PasswordResetModel PassModel)
        {
            try
            {
                _localStorageService.ClearCurrentToken();
                if (!ModelState.IsValid)
                    return View(PassModel);

                var passwordRequest = new OtpPasswordSetRequest
                {
                    Email = PassModel.Email,
                    Password = PassModel.Password,
                    Otp = PassModel.Otp
                };

                var result = await _client.SetPasswordWithOtpAsync(passwordRequest);

                await SaveToken(result.Data, false);

                return RedirectToAction("NewcomeDialysisUnit", "DialysisUnit", new { email = "dsf" });
            }
            catch (ApiException<ApiResponse<string>> ex)
            {
                var result = ex.Result;

                switch (result.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        ModelState.AddModelError("", result.Message);
                        break;
                    case HttpStatusCode.UnprocessableEntity:
                        var errors = ex.Result.Errors;
                        if (errors?.Any() ?? false)
                        {
                            foreach (var error in errors)
                            {
                                var key = (error.Split(" : ")[0]).Split('.').Last();
                                var msg = error.Split(" : ")[1];

                                ModelState.AddModelError(key, msg);
                            }
                        }
                        break;
                }

                return View(PassModel);
            }
        }

    }
}
