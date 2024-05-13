using MediatR;
using Microsoft.AspNetCore.Mvc;
using RenoCare.Core.Base;
using RenoCare.Core.Features.Patients.DTOs;
using RenoCare.Core.Features.Patients.Mediator.Queries;
using RenoCare.Core.Helpers;
using RenoCare.Domain.MetaData;
using System.Threading.Tasks;

namespace RenoCare.Api.Controllers
{
    [ApiController]
    public class PatientsController : BaseController
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public PatientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Methods

        [HttpPost(Router.PatientRouting.List)]
        public async Task<ActionResult<ApiResponse<IPagedList<PatientListItemDto>>>> GetAllPatientsAsync(
            [FromBody] GetPatientListQueryRequest request, [FromQuery] int page = 1)
        {
            request.PageIndex = page;
            return ApiResult(await _mediator.Send(request));
        }

        [HttpGet(Router.PatientRouting.Medical)]
        public async Task<ActionResult<ApiResponse<PatientDto>>> GetPatientMedicalInfoAsync([FromRoute] int id) =>
            ApiResult(await _mediator.Send(new GetPatientMedicalInfoQueryRequest { Id = id }));

        #endregion

    }
}
