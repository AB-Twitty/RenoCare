using MediatR;
using Microsoft.EntityFrameworkCore;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Features.Patients.DTOs;
using RenoCare.Core.Features.Reports.Dtos;
using RenoCare.Domain;
using RenoCare.Domain.MetaData;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.Reports.Mediator.Queries
{
    /// <summary>
    /// Represents a get patient list request with specidfied properties with a corresponding response.
    /// </summary>
    public class GetReportQueryRequest : IRequest<ApiResponse<ReportDto>>
    {
        public int Id { get; set; }
    }

    // <summary>
    /// Represents a handler for the get report query request.
    /// </summary>
    public class GetReportQueryRequestHandler : ResponseHandler,
        IRequestHandler<GetReportQueryRequest, ApiResponse<ReportDto>>
    {
        #region Fields

        private readonly IRepository<Report> _reportRepo;

        #endregion

        #region Ctor

        public GetReportQueryRequestHandler(IRepository<Report> reportRepo)
        {
            _reportRepo = reportRepo;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the get report request.
        /// </summary>
        /// <param name="request">Mediator request</param>
        /// <param name="cancellationToken">Represents a notification to cancel the request.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the authentication response.
        /// </returns>
        public async Task<ApiResponse<ReportDto>> Handle(GetReportQueryRequest request, CancellationToken cancellationToken)
        {
            var report = await _reportRepo.ApplyQueryAsync(async query =>
            {
                var b = query.Where(x => x.Id == request.Id).FirstOrDefault();

                return await query.Where(x => x.Id == request.Id)
                                  .Include(x => x.Patient)
                                  .Select(x => new ReportDto
                                  {
                                      Id = x.Id,
                                      Patient = new PatientDto
                                      {
                                          Id = x.Patient.Id,
                                          PatientName = x.Patient.User.FirstName + " " + x.Patient.User.LastName,
                                          BirthDate = DateTime.Now.AddYears(-26),
                                          Age = 26,
                                          Hypertension = x.Patient.HypertensionType.Name,
                                          Diabetes = x.Patient.DiabetesType.Name,
                                          Smoking = x.Patient.SmokingStatus.Name ?? "N/A"
                                      },
                                      CreatedDate = x.CreatedDate,
                                      LastModifiedDate = x.LastModifiedDate,
                                      SessionDate = x.MedicationRequest.AppointmentDate,
                                      Nephrologist = x.Nephrologist,
                                      DialysisDuration = x.DialysisDuration,
                                      DialysisFrequency = x.DialysisFrequency,
                                      DialysisUnitName = "Unit Name",
                                      VascularAccessType = x.VascularAccessType.ToString(),
                                      DialyzerType = x.DialyzerType.ToString(),
                                      PreWeight = x.PreWeight,
                                      PostWeight = x.PostWeight,
                                      DryWeight = x.DryWeight,
                                      PreSystolicBloodPressure = x.PreSystolicBloodPressure,
                                      DuringSystolicBloodPressure = x.DuringSystolicBloodPressure,
                                      PostSystolicBloodPressure = x.DuringSystolicBloodPressure,
                                      PreDiastolicBloodPressure = x.PreDiastolicBloodPressure,
                                      DuringDiastolicBloodPressure = x.DuringDiastolicBloodPressure,
                                      PostDiastolicBloodPressure = x.PostDiastolicBloodPressure,
                                      HeartRate = x.HeartRate,
                                      PreUrea = x.PreUrea,
                                      PostUrea = x.PostUrea,
                                      UreaReductionRatio = x.UreaReductionRatio,
                                      TotalFluidRemoval = x.TotalFluidRemoval,
                                      FluidRemovalRate = x.FluidRemovalRate,
                                      Kt_V = x.Kt_V,
                                      Creatinine = x.Creatinine,
                                      Potassium = x.Potassium,
                                      UrineOutput = x.UrineOutput,
                                      Hematocrit = x.Hematocrit,
                                      Hemoglobin = x.Hemoglobin,
                                      Albumin = x.Albumin

                                  }).FirstOrDefaultAsync();
            });

            if (report == null)
                return NotFound<ReportDto>(string.Format(Transcriptor.Response.EntityNotFound, request.Id));

            return Success(report);
        }

        #endregion
    }

}
