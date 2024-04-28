using MediatR;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Domain;
using RenoCare.Domain.MetaData;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.Smoking.Mediator.Queries
{
    /// <summary>
    /// Represents a query for smoking status list.
    /// </summary>
    public class GetSmokingStatusListQueryRequest : IRequest<ApiResponse<IList<SmokingStatus>>>
    {
        public int? Id { get; set; }
    }

    // <summary>
    /// Represents a handler for the smoking status list query request.
    /// </summary>
    public class GetSmokingStatusListQueryRequestHandler : ResponseHandler,
        IRequestHandler<GetSmokingStatusListQueryRequest, ApiResponse<IList<SmokingStatus>>>
    {
        #region Fields

        private readonly IRepository<SmokingStatus> _smokingStatusRepo;

        #endregion

        #region Ctor

        public GetSmokingStatusListQueryRequestHandler(IRepository<SmokingStatus> smokingStatusRepo)
        {
            _smokingStatusRepo = smokingStatusRepo;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the smoking status list query request.
        /// </summary>
        /// <param name="request">Mediator request</param>
        /// <param name="cancellationToken">Represents a notification to cancel the request.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the authentication response.
        /// </returns>
        public async Task<ApiResponse<IList<SmokingStatus>>> Handle(GetSmokingStatusListQueryRequest request, CancellationToken cancellationToken)
        {
            var list = await _smokingStatusRepo.GetAllAsync(query =>
            {
                if (request.Id != null)
                    query = query.Where(x => x.Id == request.Id);

                return query;
            });

            if (list.Count == 0)
                return NotFound<IList<SmokingStatus>>(string.Format(Transcriptor.Response.EntityNotFound, request.Id));

            return Success(list);
        }

        #endregion
    }
}
