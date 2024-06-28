using MediatR;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Domain;
using RenoCare.Domain.MetaData;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.Viruses.Mediator.Queries
{
    /// <summary>
    /// Represents a request to get a list of viruses
    /// </summary>
    public class GetVirusesQueryRequest : IRequest<ApiResponse<IList<Virus>>>
    {
        public int? Id { get; set; }
    }

    /// <summary>
    /// Represents a handler for the request to get a list of viruses
    /// </summary>
    public class GetVirusesQueryRequestHandler : ResponseHandler,
        IRequestHandler<GetVirusesQueryRequest, ApiResponse<IList<Virus>>>
    {
        #region Fields

        private readonly IRepository<Virus> _virusRepo;

        #endregion

        #region Ctor

        public GetVirusesQueryRequestHandler(IRepository<Virus> virusRepo)
        {
            _virusRepo = virusRepo;
        }

        #endregion

        #region Mehods 

        /// <summary>
        /// Handles the request to get a list of viruses.
        /// </summary>
        /// <param name="request">Mediator request</param>
        /// <param name="cancellationToken">Represents a notification to cancel the request.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains a list of viruses.
        /// </returns>
        public async Task<ApiResponse<IList<Virus>>> Handle(GetVirusesQueryRequest request, CancellationToken cancellationToken)
        {
            var list = await _virusRepo.GetAllAsync(query =>
            {
                if (request.Id != null)
                    query = query.Where(x => x.Id == request.Id);

                return query;
            });

            if (list.Count == 0)
                return NotFound<IList<Virus>>(string.Format(Transcriptor.Response.EntityNotFound, request.Id));

            return Success(list);
        }

        #endregion
    }
}
