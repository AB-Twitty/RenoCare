using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Features.MedicationRequests.DTOs;
using RenoCare.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.MedicationRequests.Mediator.Queries
{
    /// <summary>
    /// Represents a request to get the details of a med request
    /// </summary>
    public class GetMedicationRequestQueryRequest : IRequest<ApiResponse<MedicationRequestListItemDto>>
    {
        public int Id { get; set; }
    }

    /// <summary>
    /// Represents a handler to the request to get the details of a med request
    /// </summary>
    public class GetMedicationRequestQueryRequestHandler : ResponseHandler,
        IRequestHandler<GetMedicationRequestQueryRequest, ApiResponse<MedicationRequestListItemDto>>
    {
        #region Fields

        private readonly IRepository<MedicationRequest> _medReqRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Ctor

        public GetMedicationRequestQueryRequestHandler(IRepository<MedicationRequest> medReqRepo, IHttpContextAccessor httpContextAccessor)
        {
            _medReqRepo = medReqRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the request to get the details of a med request.
        /// </summary>
        /// <param name="request">Mediator request</param>
        /// <param name="cancellationToken">Represents a notification to cancel the request.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the details of a med equest.
        /// </returns>
        public async Task<ApiResponse<MedicationRequestListItemDto>> Handle(GetMedicationRequestQueryRequest request, CancellationToken cancellationToken)
        {
            var medReq = await _medReqRepo.Table.Where(x => x.Id == request.Id)
                .Select(x => new MedicationRequestListItemDto
                {
                    Id = x.Id,
                    PatientName = x.Patient.User.FirstName + " " + x.Patient.User.LastName,
                    DialysisUnitName = x.DialysisUnit.Name,
                    DialysisUnitId = x.DialysisUnitId,
                    Date = x.AppointmentDate,
                    Time = x.AppointmentHour,
                    Status = x.Status.Name,
                    Type = x.Type.Name,
                    ReportId = x.ReportId,
                    PatientId = x.PatientId,
                    PatientProblem = x.PatientProblem
                }).FirstOrDefaultAsync();

            if (medReq == null)
                return NotFound<MedicationRequestListItemDto>();

            if (_httpContextAccessor.HttpContext.User.IsInRole("HealthCare"))
            {
                if (!int.TryParse(_httpContextAccessor.HttpContext.Items["unitId"]?.ToString(), out int unitId))
                    return BadRequest<MedicationRequestListItemDto>();

                if (medReq.DialysisUnitId != unitId)
                    return Unauthorized<MedicationRequestListItemDto>();
            }

            return Success(medReq);
        }

        #endregion
    }
}
