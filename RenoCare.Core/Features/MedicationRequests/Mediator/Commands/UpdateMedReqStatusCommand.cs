using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Features.MedicationRequests.DTOs;
using RenoCare.Domain;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.MedicationRequests.Mediator.Commands
{
    /// <summary>
    /// Represents a request to update the status of a given medication request
    /// </summary>
    public class UpdateMedReqStatusCommandRequest : IRequest<ApiResponse<MedicationRequestListItemDto>>
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }

    /// <summary>
    /// Represents a handler for the request to update the status of a given medication request
    /// </summary>
    public class UpdateMedReqStatusCommandRequestHandler : ResponseHandler,
        IRequestHandler<UpdateMedReqStatusCommandRequest, ApiResponse<MedicationRequestListItemDto>>
    {
        #region Fields

        private readonly IRepository<Domain.MedicationRequestStatus> _medReqStatusRepo;
        private readonly IRepository<MedicationRequest> _medReqRepo;
        private readonly IRepository<Patient> _patientRepo;
        private readonly IHttpContextAccessor _ctxAccessor;

        #endregion

        #region Ctor

        public UpdateMedReqStatusCommandRequestHandler(IRepository<Domain.MedicationRequestStatus> medReqStatusRepo,
            IRepository<MedicationRequest> medReqRepo, IHttpContextAccessor ctxAccessor, IRepository<Patient> patientRepo)
        {
            _medReqStatusRepo = medReqStatusRepo;
            _medReqRepo = medReqRepo;
            _ctxAccessor = ctxAccessor;
            _patientRepo = patientRepo;
        }

        #endregion

        #region Methods

        public async Task<ApiResponse<MedicationRequestListItemDto>> Handle(UpdateMedReqStatusCommandRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Status))
                return BadRequest<MedicationRequestListItemDto>();

            var status = (await _medReqStatusRepo.GetAllAsync(q => q.Where(s => s.Name == request.Status))).FirstOrDefault();

            if (status == null)
                return BadRequest<MedicationRequestListItemDto>();

            var medReq = await _medReqRepo.GetByIdAsync(request.Id);

            if (medReq == null)
                return NotFound<MedicationRequestListItemDto>();

            if (_ctxAccessor.HttpContext.User.IsInRole("Patient"))
            {
                if (status.Name != "Cancelled")
                    return BadRequest<MedicationRequestListItemDto>();

                var curr_user = _ctxAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                var patientId = await _patientRepo.ApplyQueryAsync(async q =>
                    await q.Where(x => x.UserId == curr_user).Select(x => x.Id).FirstOrDefaultAsync());

                if (medReq.PatientId != patientId)
                    return Unauthorized<MedicationRequestListItemDto>();
            }

            else if (_ctxAccessor.HttpContext.User.IsInRole("HealthCare"))
            {
                if (status.Name != "Rejected" && status.Name != "Upcoming")
                    return BadRequest<MedicationRequestListItemDto>();

                if (!int.TryParse(_ctxAccessor.HttpContext.Items["unitId"].ToString(), out int unitId))
                    return BadRequest<MedicationRequestListItemDto>();

                if (medReq.DialysisUnitId != unitId)
                    return Unauthorized<MedicationRequestListItemDto>();
            }

            medReq.StatusId = status.Id;
            await _medReqRepo.SaveAsync();

            return Success(new MedicationRequestListItemDto
            {
                Id = medReq.Id,
                //PatientName = medReq.Patient.User.FirstName + " " + medReq.Patient.User.LastName,
                //DialysisUnitName = medReq.DialysisUnit.Name,
                DialysisUnitId = medReq.DialysisUnitId,
                Date = medReq.AppointmentDate,
                //Time = new DateTime(medReq.Session.Time.Ticks).ToString("hh:mm tt"),
                Status = medReq.Status.Name,
                //Type = medReq.Type.Name,
                ReportId = medReq.ReportId,
                PatientId = medReq.PatientId,
                PatientProblem = medReq.PatientProblem
            });
        }

        #endregion
    }
}
