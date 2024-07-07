using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RenoCare.Core.Base;
using RenoCare.Core.Features.Authentication.Contracts.Models;
using RenoCare.Core.Features.Authentication.Mediator.Commands;
using RenoCare.Core.Features.Authentication.Mediator.Queries;
using RenoCare.Core.Features.HealthCareProviders.Mediator.Commands;
using RenoCare.Domain.MetaData;
using System.Threading.Tasks;

namespace RenoCare.Api.Controllers
{
    [ApiController]
    public class UserController : BaseController
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Actions

        [HttpPost(Router.AccountRouting.Login)]
        public async Task<ActionResult<ApiResponse<AuthResponse>>> LoginAsync(AuthRequest request) =>
            ApiResult(await _mediator.Send(new LoginCommandRequest(request)));

        [HttpGet(Router.AccountRouting.SendEmailConfirmation)]
        public async Task<ActionResult<ApiResponse<string>>> SendEmailConfirmationAsync(string userId) =>
            ApiResult(await _mediator.Send(new SendEmailConfirmationCommandRequest { UserId = userId }));

        [HttpGet(Router.AccountRouting.ConfirmEmail)]
        public async Task<ActionResult<ApiResponse<string>>> ConfirmEmailAsync(string userId, string code) =>
            ApiResult(await _mediator.Send(new ConfirmEmailCommandRequest { UserId = userId, EncodedToken = code }));

        [HttpPost(Router.HealthCareProviderRouting.CreateHealthCareUser)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<string>>> AddHealthCareProviderUserAsync([FromForm] string email) =>
            ApiResult(await _mediator.Send(new CreateHealthCareProviderUserCommandRequest { Email = email }));

        [HttpPost(Router.AccountRouting.SetPasswordWithOtp)]
        public async Task<ActionResult<ApiResponse<AuthResponse>>> SetPasswordWithOtpAsync(OtpPasswordSetRequest passRequest) =>
            ApiResult(await _mediator.Send(new SetPasswordWithOtpCommandRequest { PasswordModel = passRequest }));

        [HttpGet(Router.AccountRouting.Profile)]
        public async Task<ActionResult<ApiResponse<UserInfo>>> GetUserProfile(string userId) =>
            ApiResult(await _mediator.Send(new GetProfileQueryRequest { Id = userId }));

        #endregion
    }
}
