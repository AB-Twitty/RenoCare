using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RenoCare.Core.Base;
using RenoCare.Core.Features.Home.Dtos;
using RenoCare.Core.Features.Home.Mediator.Queries;
using RenoCare.Domain.MetaData;
using System.Threading.Tasks;

namespace RenoCare.Api.Controllers
{
    public class HomeController : BaseController
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Methods

        [HttpGet(Router.HomeRouting.Dashboard)]
        [Authorize(Roles = "Admin, HealthCare")]
        public async Task<ActionResult<ApiResponse<DashboardDto>>> GetDashboardDataAsync() =>
            ApiResult(await _mediator.Send(new GetDashboardDataQueryRequest()));

        #endregion
    }
}
