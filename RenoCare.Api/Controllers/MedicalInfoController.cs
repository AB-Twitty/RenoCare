using MediatR;
using Microsoft.AspNetCore.Mvc;
using RenoCare.Core.Base;
using RenoCare.Core.Features.DiabetesTypes.Mediator.Queries;
using RenoCare.Core.Features.HypertensionTypes.Mediator.Queries;
using RenoCare.Core.Features.Smoking.Mediator.Queries;
using RenoCare.Domain;
using RenoCare.Domain.MetaData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenoCare.Api.Controllers
{
    public class MedicalInfoController : BaseController
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public MedicalInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Methods

        [HttpGet(Router.PatientsMedicalInfo.DiabetesTypes)]
        public async Task<ActionResult<ApiResponse<IList<DiabetesType>>>> GetDiabetesTypesAsync([FromQuery] int? id) =>
            ApiResult(await _mediator.Send(new GetDiabetesTypesQueryRequest { Id = id }));

        [HttpGet(Router.PatientsMedicalInfo.HypertensionTypes)]
        public async Task<ActionResult<ApiResponse<IList<HypertensionType>>>> GetHypertensionTypesAsync([FromQuery] int? id) =>
            ApiResult(await _mediator.Send(new GetHypertensionTypesQueryRequest { Id = id }));

        [HttpGet(Router.PatientsMedicalInfo.SmokingStatuses)]
        public async Task<ActionResult<ApiResponse<IList<SmokingStatus>>>> GetSmokingStatusListAsync([FromQuery] int? id) =>
            ApiResult(await _mediator.Send(new GetSmokingStatusListQueryRequest { Id = id }));

        #endregion
    }
}
