using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Features.MedicationRequests.DTOs;
using RenoCare.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.MedicationRequests.Mediator.Queries
{
    /// <summary>
    /// Represents a request to gel all Medication Requests for a given patient
    /// </summary>
    public class GetMedReqAllForPatientQueryRequest : IRequest<ApiResponse<IList<MedicationRequestListItemDto>>>
    {
        public int? PatientId { get; set; }
    }

    /// <summary>
    /// Represents a handler for the request to gel all Medication Requests for a given patient
    /// </summary>
    public class GetMedReqAllForPatientQueryRequestHandler : ResponseHandler,
         IRequestHandler<GetMedReqAllForPatientQueryRequest, ApiResponse<IList<MedicationRequestListItemDto>>>
    {
        #region Fields

        private readonly IRepository<MedicationRequest> _medReqRepo;
        private readonly IHttpContextAccessor _ctxAccessor;

        #endregion

        #region Ctor

        public GetMedReqAllForPatientQueryRequestHandler(IHttpContextAccessor ctxAccessor, IRepository<MedicationRequest> medReqRepo)
        {
            _ctxAccessor = ctxAccessor;
            _medReqRepo = medReqRepo;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the request to gel all Medication Requests for a given patient.
        /// </summary>
        /// <param name="request">Mediator request</param>
        /// <param name="cancellationToken">Represents a notification to cancel the request.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains a list of med req.
        /// </returns>
        public async Task<ApiResponse<IList<MedicationRequestListItemDto>>> Handle(GetMedReqAllForPatientQueryRequest request, CancellationToken cancellationToken)
        {
            if (_ctxAccessor.HttpContext.User.IsInRole("Patient"))
            {
                var curr_user = _ctxAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)
                    ?.Value;

                IList<MedicationRequestListItemDto> list_with_patient = await _medReqRepo.ApplyQueryAsync(async qry =>
                {
                    return await qry.Where(x => x.Patient.UserId == curr_user)
                        .Select(x => new MedicationRequestListItemDto
                        {
                            Id = x.Id,
                            PatientName = x.Patient.User.FirstName + " " + x.Patient.User.LastName,
                            DialysisUnitName = x.DialysisUnit.Name,
                            DialysisUnitId = x.DialysisUnitId,
                            Date = x.AppointmentDate,
                            Time = new DateTime(x.Session.Time.Ticks).ToString("hh:mm tt"),
                            Status = x.Status.Name,
                            Type = x.Type.Name,
                            ReportId = x.ReportId,
                            PatientId = x.PatientId,
                            PatientProblem = x.PatientProblem
                        }).ToListAsync();
                });

                return Success(list_with_patient);
            }

            if (request.PatientId == null)
                return BadRequest<IList<MedicationRequestListItemDto>>();

            else if (_ctxAccessor.HttpContext.User.IsInRole("HealthCare"))
            {
                if (!int.TryParse(_ctxAccessor.HttpContext.Items["unitId"]?.ToString(), out int unitId))
                    return BadRequest<IList<MedicationRequestListItemDto>>();

                var allowed_statuses = new List<string> { "Completed", "Pending", "Upcoming" };

                if (!await _medReqRepo.Table.AnyAsync(x => x.PatientId == request.PatientId && allowed_statuses.Contains(x.Status.Name)))
                    return Unauthorized<IList<MedicationRequestListItemDto>>();

            }

            IList<MedicationRequestListItemDto> list = await _medReqRepo.ApplyQueryAsync(async qry =>
            {
                return await qry.Where(x => x.PatientId == request.PatientId)
                    .Select(x => new MedicationRequestListItemDto
                    {
                        Id = x.Id,
                        PatientName = x.Patient.User.FirstName + " " + x.Patient.User.LastName,
                        DialysisUnitName = x.DialysisUnit.Name,
                        DialysisUnitId = x.DialysisUnitId,
                        Date = x.AppointmentDate,
                        Time = new DateTime(x.Session.Time.Ticks).ToString("hh:mm tt"),
                        Status = x.Status.Name,
                        Type = x.Type.Name,
                        ReportId = x.ReportId,
                        PatientId = x.PatientId,
                        PatientProblem = x.PatientProblem
                    }).ToListAsync();
            });

            return Success(list);
        }

        #endregion
    }
}
