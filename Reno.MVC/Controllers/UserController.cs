using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reno.MVC.Models.User.Auth;
using Reno.MVC.Services.Base;
using Reno.MVC.Services.Base.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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

        [HttpGet("Login")]
        public virtual IActionResult LoginAsync(string returnUrl = null)
        {
            return View();
        }

        [HttpPost("Login")]
        public virtual async Task<IActionResult> LoginAsync(LoginModel login, string returnUrl = null)
        {
            try
            {
                await _httpContextAccessor.HttpContext.SignOutAsync();
                _localStorageService.ClearStorage(new List<string> { "Token" });

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

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenContent = tokenHandler.ReadJwtToken(result.Data.AccessToken);

                var claims = tokenContent.Claims.ToList();

                var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user,
                    new AuthenticationProperties
                    {
                        IsPersistent = login.RememberMe,
                        ExpiresUtc = login.RememberMe ? DateTime.UtcNow.AddDays(7) : DateTime.UtcNow.AddHours(1)
                    });

                _localStorageService.SetStorageValue("Token", result.Data.AccessToken);

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
            await _httpContextAccessor.HttpContext.SignOutAsync();
            _localStorageService.ClearStorage(new List<string> { "Token" });

            return RedirectToAction("Index", "Home");
        }


    }
}
