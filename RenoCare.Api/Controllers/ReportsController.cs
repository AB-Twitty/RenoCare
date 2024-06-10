using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RenoCare.Core.Base;
using RenoCare.Core.Features.Reports.Dtos;
using RenoCare.Core.Features.Reports.Mediator.Commands;
using RenoCare.Core.Features.Reports.Mediator.Queries;
using RenoCare.Domain.MetaData;
using System.Threading.Tasks;

namespace RenoCare.Api.Controllers
{
    [ApiController]
    public class ReportsController : BaseController
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Methods

        [HttpGet(Router.ReportRouting.GetById)]
        public async Task<ActionResult<ApiResponse<ReportDto>>> GetReportAsync([FromQuery] int id) =>
            ApiResult(await _mediator.Send(new GetReportQueryRequest { Id = id }));


        [HttpPost(Router.ReportRouting.Create)]
        [Authorize(Roles = "HealthCare")]
        public async Task<ActionResult<ApiResponse<ReportDto>>> CreateReportAsync([FromBody] ReportDto report) =>
            ApiResult(await _mediator.Send(new CreateReportCommandRequest { Report = report }));

        #endregion
    }
}
