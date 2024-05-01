using MediatR;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Domain;
using RenoCare.Domain.MetaData;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.MedicationRequestTypes.Mediator.Queries
{
    /// <summary>
    /// Represents a query for medication request types list.
    /// </summary>
    public class GetMedicationRequestTypesQueryRequest : IRequest<ApiResponse<IList<MedicationRequestType>>>
    {
        public int? Id { get; set; }
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// Represents a handler for the medication request types list query request.
    /// </summary>
    public class GetMedicationRequestTypesQueryHandler : ResponseHandler,
        IRequestHandler<GetMedicationRequestTypesQueryRequest, ApiResponse<IList<MedicationRequestType>>>
    {
        #region Fields

        private readonly IRepository<MedicationRequestType> _MedicationRequestTypesRepo;

        #endregion

        #region Ctor

        public GetMedicationRequestTypesQueryHandler(IRepository<MedicationRequestType> medicationRequestTypesRepo)
        {
            _MedicationRequestTypesRepo = medicationRequestTypesRepo;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the medication request types list query request.
        /// </summary>
        /// <param name="request">Mediator request</param>
        /// <param name="cancellationToken">Represents a notification to cancel the request.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the response.
        /// </returns>
        public async Task<ApiResponse<IList<MedicationRequestType>>> Handle(GetMedicationRequestTypesQueryRequest request, CancellationToken cancellationToken)
        {
            var notFound = false;

            var list = await _MedicationRequestTypesRepo.GetAllAsync(query =>
            {
                if (request.Id != null)
                {
                    query = query.Where(x => x.Id == request.Id);
                    notFound = query.Any();
                }

                return query.Where(x => x.IsActive == request.IsActive);
            });

            if (notFound)
                return NotFound<IList<MedicationRequestType>>(string.Format(Transcriptor.Response.EntityNotFound, request.Id));

            return Success(list);
        }

        #endregion
    }
}
