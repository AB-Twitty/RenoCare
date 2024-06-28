using MediatR;
using Microsoft.AspNetCore.Mvc;
using RenoCare.Core.Base;
using RenoCare.Core.Features.Viruses.Mediator.Queries;
using RenoCare.Domain;
using RenoCare.Domain.MetaData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenoCare.Api.Controllers
{
    public class VirusesController : BaseController
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public VirusesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Methods

        [HttpGet(Router.VirusesRouting.List)]
        public async Task<ActionResult<ApiResponse<IList<Virus>>>> GetAmentitiesAsync([FromQuery] int? id = null) =>
            ApiResult(await _mediator.Send(new GetVirusesQueryRequest { Id = id }));

        #endregion
    }
}
