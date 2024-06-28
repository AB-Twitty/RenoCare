using MediatR;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Extensions;
using RenoCare.Core.Features.Reviews.Dtos;
using RenoCare.Core.Helpers;
using RenoCare.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.Reviews.Mediator.Queries
{
    /// <summary>
    /// Represents a request to get the reviews of a given dialysis unit.
    /// </summary>
    public class GetReviewsForDialysisUnitQueryRequest : IRequest<ApiResponse<IPagedList<ReviewDto>>>
    {
        public int UnitId { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = int.MaxValue;
    }

    /// <summary>
    /// Represents a handler for the request to get a paged list of reviews for a given dialysis unit
    /// </summary>
    public class GetReviewsForDialysisUnitQueryRequestHandler : ResponseHandler,
        IRequestHandler<GetReviewsForDialysisUnitQueryRequest, ApiResponse<IPagedList<ReviewDto>>>
    {
        #region Fields 

        private readonly IRepository<Review> _reviewRepo;

        #endregion

        #region Ctor

        public GetReviewsForDialysisUnitQueryRequestHandler(IRepository<Review> reviewRepo)
        {
            _reviewRepo = reviewRepo;
        }

        #endregion

        #region Methods 

        /// <summary>
        /// Handles the request to get a paged list of reviews for a given dialysis unit.
        /// </summary>
        /// <param name="request">Mediator request</param>
        /// <param name="cancellationToken">Represents a notification to cancel the request.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains a paged list of reviews.
        /// </returns>
        public async Task<ApiResponse<IPagedList<ReviewDto>>> Handle(GetReviewsForDialysisUnitQueryRequest request, CancellationToken cancellationToken)
        {
            var list = await _reviewRepo.ApplyQueryAsync(async qry =>
            {
                return await qry.Where(x => x.DialysisUnitId == request.UnitId && !string.IsNullOrEmpty(x.Comment))
                            .Select(x => new ReviewDto
                            {
                                PatientName = x.Patient.User.FirstName + " " + x.Patient.User.LastName,
                                Comment = x.Comment,
                                CreationDate = x.CreationDate,
                                Rating = x.Rating,
                            })
                            .OrderBy(x => x.CreationDate).ToPagedListAsync(request.PageIndex, request.PageSize);
            });

            return Success(list);
        }

        #endregion
    }
}
