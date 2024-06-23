using MediatR;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Domain;
using RenoCare.Domain.MetaData;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.DiabetesTypes.Mediator.Queries
{
    /// <summary>
    /// Represents a query for diabetes types.
    /// </summary>
    public class GetDiabetesTypesQueryRequest : IRequest<ApiResponse<IList<DiabetesType>>>
    {
        public int? Id { get; set; }
    }

    // <summary>
    /// Represents a handler for the diabetes types query request.
    /// </summary>
    public class GetDiabetesTypesQueryRequestHandler : ResponseHandler,
        IRequestHandler<GetDiabetesTypesQueryRequest, ApiResponse<IList<DiabetesType>>>
    {
        #region Fields

        private readonly IRepository<DiabetesType> _diabetesTypesRepo;

        #endregion

        #region Ctor

        public GetDiabetesTypesQueryRequestHandler(IRepository<DiabetesType> diabetesTypesRepo)
        {
            _diabetesTypesRepo = diabetesTypesRepo;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the diabetes types query request.
        /// </summary>
        /// <param name="request">Mediator request</param>
        /// <param name="cancellationToken">Represents a notification to cancel the request.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the authentication response.
        /// </returns>
        public async Task<ApiResponse<IList<DiabetesType>>> Handle(GetDiabetesTypesQueryRequest request, CancellationToken cancellationToken)
        {
            var list = await _diabetesTypesRepo.GetAllAsync(query =>
            {
                if (request.Id != null)
                    query = query.Where(x => x.Id == request.Id);

                return query;
            });

            if (list.Count == 0)
                return NotFound<IList<DiabetesType>>(string.Format(Transcriptor.Response.EntityNotFound, request.Id));

            return Success(list);
        }

        #endregion
    }
}
