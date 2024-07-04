using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RenoCare.Core.Base;
using RenoCare.Core.Features.MedicationRequests.DTOs;
using RenoCare.Core.Features.MedicationRequests.Mediator.Commands;
using RenoCare.Core.Features.MedicationRequests.Mediator.Queries;
using RenoCare.Core.Features.MedicationRequestStatus.Mediator.Queries;
using RenoCare.Core.Features.MedicationRequestTypes.Mediator.Queries;
using RenoCare.Core.Helpers;
using RenoCare.Domain;
using RenoCare.Domain.MetaData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenoCare.Api.Controllers
{
    [ApiController]
    public class MedicationRequestsController : BaseController
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public MedicationRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Methods

        [HttpPost(Router.MedicationRequestRouting.List)]
        [Authorize(Roles = "Admin, HealthCare")]
        public async Task<ActionResult<ApiResponse<IPagedList<MedicationRequestListItemDto>>>> GetMedicationRequestsAsync(
            [FromBody] GetMedicationRequestsQueryRequest request, [FromQuery] int page = 1)
        {
            request.PageIndex = page;
            return ApiResult(await _mediator.Send(request));
        }

        [HttpGet(Router.MedicationRequestRouting.Details)]
        [Authorize(Roles = "Admin, HealthCare")]
        public async Task<ActionResult<ApiResponse<MedicationRequestListItemDto>>> GetMedicationRequestDetailsAsync([FromRoute] int requestId) =>
            ApiResult(await _mediator.Send(new GetMedicationRequestQueryRequest { Id = requestId }));

        [HttpGet(Router.MedicationRequestRouting.Status)]
        [Authorize(Roles = "Admin, HealthCare, Patient")]
        public async Task<ActionResult<ApiResponse<IList<MedicationRequestStatus>>>> GetMedicationRequestStatusListAsync(
            [FromQuery] int? id = null)
        {
            return ApiResult(await _mediator.Send(new GetMedicationRequestStatusListQueryRequest { Id = id }));
        }

        [HttpGet(Router.MedicationRequestRouting.Types)]
        [Authorize(Roles = "Admin, HealthCare, Patient")]
        public async Task<ActionResult<ApiResponse<IList<MedicationRequestType>>>> GetMedicationRequestTypesAsync(
            [FromQuery] int? id = null, bool active = true)
        {
            return ApiResult(await _mediator.Send(new GetMedicationRequestTypesQueryRequest { Id = id, IsActive = active }));
        }

        [HttpGet(Router.MedicationRequestRouting.List)]
        [Authorize(Roles = "Admin, HealthCare, Patient")]
        public async Task<ActionResult<ApiResponse<IList<MedicationRequestListItemDto>>>> GetMedReqAllForPatientAsync([FromQuery] int? patientId) =>
            ApiResult(await _mediator.Send(new GetMedReqAllForPatientQueryRequest { PatientId = patientId }));

        [HttpPost(Router.MedicationRequestRouting.StatusUpdate)]
        [Authorize(Roles = "HealthCare, Patient")]
        public async Task<ActionResult<ApiResponse<MedicationRequestListItemDto>>> UpdateMedReqStatusAsync(UpdateMedReqStatusCommandRequest req) =>
            ApiResult(await _mediator.Send(req));

        [HttpPost(Router.MedicationRequestRouting.Schedule)]
        [Authorize(Roles = "Patient")]
        public async Task<ActionResult<ApiResponse<string>>> ScheduleMedReqAsync([FromBody] ScheduleMedReqCommandRequest req) =>
            ApiResult(await _mediator.Send(req));

        [HttpGet(Router.MedicationRequestRouting.ForPatient)]
        [Authorize(Roles = "Patient")]
        public async Task<ActionResult<ApiResponse<IList<MedicationRequestDetailsDto>>>> GetMedReqsForPatientAsync() =>
            ApiResult(await _mediator.Send(new GetMedicationRequestDetailsQueryRequest()));

        #endregion
    }
}
