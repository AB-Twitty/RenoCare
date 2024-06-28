using MediatR;
using Microsoft.AspNetCore.Mvc;
using RenoCare.Core.Base;
using RenoCare.Core.Features.Reviews.Dtos;
using RenoCare.Core.Features.Reviews.Mediator.Commands;
using RenoCare.Core.Features.Reviews.Mediator.Queries;
using RenoCare.Core.Helpers;
using RenoCare.Domain.MetaData;
using System.Threading.Tasks;

namespace RenoCare.Api.Controllers
{
    public class ReviewsController : BaseController
    {
        #region Fields 

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public ReviewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Methods

        [HttpGet(Router.ReviewRouting.ListForUnit)]
        public async Task<ActionResult<ApiResponse<IPagedList<ReviewDto>>>> GetReviewsForDialysisUnitAsync([FromQuery] int unitId, int page = 1) =>
            ApiResult(await _mediator.Send(new GetReviewsForDialysisUnitQueryRequest { UnitId = unitId, PageIndex = page, PageSize = 10 }));

        [HttpPatch(Router.ReviewRouting.Create)]
        public async Task<ActionResult<ApiResponse<string>>> CreateReviewAsync([FromBody] CreateReviewCommandRequest req) =>
            ApiResult(await _mediator.Send(req));

        #endregion
    }
}
