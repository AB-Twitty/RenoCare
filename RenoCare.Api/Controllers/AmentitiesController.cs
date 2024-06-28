using MediatR;
using Microsoft.AspNetCore.Mvc;
using RenoCare.Core.Base;
using RenoCare.Core.Features.Amentities.Mediator.Queries;
using RenoCare.Domain;
using RenoCare.Domain.MetaData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenoCare.Api.Controllers
{
    [ApiController]
    public class AmentitiesController : BaseController
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region

        public AmentitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Methods

        [HttpGet(Router.AmentitiesRouting.List)]
        public async Task<ActionResult<ApiResponse<IList<Amenity>>>> GetAmentitiesAsync([FromQuery] int? id = null) =>
            ApiResult(await _mediator.Send(new GetAmentitiesQueryRequest { Id = id }));

        #endregion
    }
}
