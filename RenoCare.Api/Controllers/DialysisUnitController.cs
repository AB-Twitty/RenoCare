using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RenoCare.Core.Base;
using RenoCare.Core.Features.DialysisUnits.Dtos;
using RenoCare.Core.Features.DialysisUnits.Mediator.Commands;
using RenoCare.Core.Features.DialysisUnits.Mediator.Queries;
using RenoCare.Core.Helpers;
using RenoCare.Domain.MetaData;
using System.Threading.Tasks;

namespace RenoCare.Api.Controllers
{
    [ApiController]
    public class DialysisUnitController : BaseController
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DialysisUnitController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Methods

        [HttpPost(Router.HealthCareProviderRouting.NewCome)]
        public async Task<ActionResult<ApiResponse<string>>> CreateNewcomeDialysisUnitAsync(
            [FromBody] CreateNewcomeDialysisUnitCommandRequest req) =>
            ApiResult(await _mediator.Send(req));

        [HttpGet(Router.DialysisUnitRouting.Details)]
        [Authorize(Roles = "Admin, HealthCare")]
        public async Task<ActionResult<ApiResponse<DialysisUnitDetailsDto>>> GetDialysisUnitDetailsAsync(int id) =>
            ApiResult(await _mediator.Send(new GetDialysisUnitDetailsQueryRequest { Id = id }));

        [HttpPost(Router.DialysisUnitRouting.List)]
        [Authorize(Roles = "Admin, HealthCare")]
        public async Task<ActionResult<ApiResponse<IPagedList<DialysisUnitListItemDto>>>> GetDialysisUnitsAsync(
            [FromBody] GetDialysisUnitsListQueryRequest request, [FromQuery] int page = 1)
        {
            request.PageIndex = page;
            return ApiResult(await _mediator.Send(request));
        }

        [HttpGet(Router.DialysisUnitRouting.ListForPatients)]
        [Authorize(Roles = "Patient")]
        public async Task<ActionResult<ApiResponse<IPagedList<DialysisUnitSimpleListItemDto>>>>
            GetDialysisUnitsForPatientsAsync([FromQuery] int page = 1, int pageSize = 20, string search = null,
                string treatment = null, string viruses = null, string amenities = null, int? day = null, string sortBy = "Id") =>
            ApiResult(await _mediator.Send(new GetDialysisUnitsForPatientsQueryRequest
            {
                PageIndex = page,
                PageSize = pageSize,
                Treatment = treatment,
                Viruses = viruses,
                Amenities = amenities,
                Search = search,
                Day = day,
                SortBy = sortBy
            }));

        #endregion
    }
}
