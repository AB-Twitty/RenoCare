using MediatR;
using Microsoft.AspNetCore.Mvc;
using RenoCare.Core.Features.Authentication.Contracts.Models;
using RenoCare.Core.Features.Authentication.Mediator.Commands;
using RenoCare.Domain.MetaData;
using System.Threading.Tasks;

namespace RenoCare.Api.Controllers
{
    [ApiController]
    public class AccountController : BaseController
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Actions

        [HttpPost(Router.AccountRouting.Login)]
        public async Task<IActionResult> LoginAsync(AuthRequest request) =>
            ApiResult(await _mediator.Send(new LoginCommandRequest(request)));

        #endregion
    }
}
