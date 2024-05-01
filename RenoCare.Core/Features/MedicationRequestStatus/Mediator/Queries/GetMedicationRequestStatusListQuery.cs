using MediatR;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Domain.MetaData;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.MedicationRequestStatus.Mediator.Queries
{
    /// <summary>
    /// Represents a query for medication request status list.
    /// </summary>
    public class GetMedicationRequestStatusListQueryRequest : IRequest<ApiResponse<IList<Domain.MedicationRequestStatus>>>
    {
        public int? Id { get; set; }
    }

    /// <summary>
    /// Represents a handler for the medication request status list query request.
    /// </summary>
    public class GetMedicationRequestStatusListQueryRequestHandler : ResponseHandler,
        IRequestHandler<GetMedicationRequestStatusListQueryRequest, ApiResponse<IList<Domain.MedicationRequestStatus>>>
    {
        #region Fields

        private readonly IRepository<Domain.MedicationRequestStatus> _medicationRequestStatusRepo;

        #endregion

        #region Ctor

        public GetMedicationRequestStatusListQueryRequestHandler(IRepository<Domain.MedicationRequestStatus> medicationRequestStatusRepo)
        {
            _medicationRequestStatusRepo = medicationRequestStatusRepo;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the medication request status list query request.
        /// </summary>
        /// <param name="request">Mediator request</param>
        /// <param name="cancellationToken">Represents a notification to cancel the request.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the response.
        /// </returns>
        public async Task<ApiResponse<IList<Domain.MedicationRequestStatus>>> Handle(GetMedicationRequestStatusListQueryRequest request, CancellationToken cancellationToken)
        {
            var list = await _medicationRequestStatusRepo.GetAllAsync(query =>
            {
                if (request.Id != null)
                    query = query.Where(x => x.Id == request.Id);

                return query;
            });

            if (list.Count == 0)
                return NotFound<IList<Domain.MedicationRequestStatus>>(string.Format(Transcriptor.Response.EntityNotFound, request.Id));

            return Success(list);
        }

        #endregion
    }
}
