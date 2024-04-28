using MediatR;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Domain;
using RenoCare.Domain.MetaData;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.HypertensionTypes.Mediator.Queries
{
    /// <summary>
    /// Represents a query for hypertension types.
    /// </summary>
    public class GetHypertensionTypesQueryRequest : IRequest<ApiResponse<IList<HypertensionType>>>
    {
        public int? Id { get; set; }
    }

    // <summary>
    /// Represents a handler for the hypertension types query request.
    /// </summary>
    public class GetHypertensionTypesQueryRequestHandler : ResponseHandler,
        IRequestHandler<GetHypertensionTypesQueryRequest, ApiResponse<IList<HypertensionType>>>
    {
        #region Fields

        private readonly IRepository<HypertensionType> _hypertensionTypesRepo;

        #endregion

        #region Ctor

        public GetHypertensionTypesQueryRequestHandler(IRepository<HypertensionType> hypertensionTypesRepo)
        {
            _hypertensionTypesRepo = hypertensionTypesRepo;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the hypertension types query request.
        /// </summary>
        /// <param name="request">Mediator request</param>
        /// <param name="cancellationToken">Represents a notification to cancel the request.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the authentication response.
        /// </returns>
        public async Task<ApiResponse<IList<HypertensionType>>> Handle(GetHypertensionTypesQueryRequest request, CancellationToken cancellationToken)
        {
            var list = await _hypertensionTypesRepo.GetAllAsync(query =>
            {
                if (request.Id != null)
                    query = query.Where(x => x.Id == request.Id);

                return query;
            });

            if (list.Count == 0)
                return NotFound<IList<HypertensionType>>(string.Format(Transcriptor.Response.EntityNotFound, request.Id));

            return Success(list);
        }

        #endregion
    }
}
