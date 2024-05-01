using MediatR;
using Microsoft.AspNetCore.Mvc;
using RenoCare.Core.Base;
using RenoCare.Core.Features.MedicationRequests.Mediator.Queries;
using RenoCare.Core.Features.MedicationRequestStatus.Mediator.Queries;
using RenoCare.Core.Features.MedicationRequestTypes.Mediator.Queries;
using RenoCare.Core.Features.Patients.DTOs;
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
        public async Task<ActionResult<ApiResponse<IPagedList<MedicationRequestListItemDto>>>> GetMedicationRequestsAsync(
            [FromBody] GetMedicationRequestsQueryRequest request, [FromQuery] int page = 1)
        {
            request.PageIndex = page;
            return ApiResult(await _mediator.Send(request));
        }

        [HttpGet(Router.MedicationRequestRouting.Status)]
        public async Task<ActionResult<ApiResponse<IList<MedicationRequestStatus>>>> GetMedicationRequestStatusListAsync(
            [FromQuery] int? id = null)
        {
            return ApiResult(await _mediator.Send(new GetMedicationRequestStatusListQueryRequest { Id = id }));
        }

        [HttpGet(Router.MedicationRequestRouting.Types)]
        public async Task<ActionResult<ApiResponse<IList<MedicationRequestType>>>> GetMedicationRequestTypesAsync(
            [FromQuery] int? id = null, bool active = true)
        {
            return ApiResult(await _mediator.Send(new GetMedicationRequestTypesQueryRequest { Id = id, IsActive = active }));
        }

        #endregion
    }
}
