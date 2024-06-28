using MediatR;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Domain;
using RenoCare.Domain.MetaData;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.Amentities.Mediator.Queries
{
    public class GetAmentitiesQueryRequest : IRequest<ApiResponse<IList<Amenity>>>
    {
        public int? Id { get; set; }
    }

    public class GetAmentitiesQueryRequestHandler : ResponseHandler,
        IRequestHandler<GetAmentitiesQueryRequest, ApiResponse<IList<Amenity>>>
    {
        #region Fields

        private IRepository<Amenity> _amentitiesRepo;

        #endregion

        #region

        public GetAmentitiesQueryRequestHandler(IRepository<Amenity> amentitiesRepo)
        {
            _amentitiesRepo = amentitiesRepo;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the amentities query request.
        /// </summary>
        /// <param name="request">Mediator request</param>
        /// <param name="cancellationToken">Represents a notification to cancel the request.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains a list of amentities.
        /// </returns>
        public async Task<ApiResponse<IList<Amenity>>> Handle(GetAmentitiesQueryRequest request, CancellationToken cancellationToken)
        {
            var list = await _amentitiesRepo.GetAllAsync(query =>
            {
                if (request.Id != null)
                    query = query.Where(x => x.Id == request.Id);

                return query;
            });

            if (list.Count == 0)
                return NotFound<IList<Amenity>>(string.Format(Transcriptor.Response.EntityNotFound, request.Id));

            return Success(list);
        }

        #endregion
    }
}
