﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RenoCare.Core.Base;
using RenoCare.Core.Features.Reports.Dtos;
using RenoCare.Core.Features.Reports.Mediator.Commands;
using RenoCare.Core.Features.Reports.Mediator.Queries;
using RenoCare.Core.Helpers;
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
        [Authorize(Roles = "Admin, HealthCare")]
        public async Task<ActionResult<ApiResponse<ReportDto>>> GetReportAsync([FromQuery] int id) =>
            ApiResult(await _mediator.Send(new GetReportQueryRequest { Id = id }));


        [HttpPost(Router.ReportRouting.Create)]
        [Authorize(Roles = "HealthCare")]
        public async Task<ActionResult<ApiResponse<ReportDto>>> CreateReportAsync([FromBody] ReportDto report) =>
            ApiResult(await _mediator.Send(new CreateReportCommandRequest { Report = report }));

        [HttpGet("report/pdf/{id}")]
        [Authorize(Roles = "Admin, HealthCare, Patient")]
        public async Task<IActionResult> GetReportAsPdfAsync(int id)
        {
            var response = await _mediator.Send(new GetReportAsPdfQueryRequest { ReportId = id });

            return File(response.File, "application/pdf", response.FileName);
        }

        [HttpPost(Router.ReportRouting.List)]
        [Authorize(Roles = "Admin, HealthCare")]
        public async Task<ActionResult<ApiResponse<IPagedList<ReportDto>>>> GetReportsListAsync([FromBody] GetReportsListQueryRequest req,
            [FromQuery] int page = 1)
        {
            req.PageIndex = page;
            return ApiResult(await _mediator.Send(req));
        }

        [HttpGet(Router.ReportRouting.AllForPatient)]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetAllPatientReportsAsync()
        {
            var res = await _mediator.Send(new GetAllPatientReportQueryRequest());

            return File(res.File, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", res.FileName);
        }


        #endregion
    }
}
