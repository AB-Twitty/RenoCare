using MediatR;
using Microsoft.EntityFrameworkCore;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Features.Patients.DTOs;
using RenoCare.Core.Features.Reports.Dtos;
using RenoCare.Core.Helpers;
using RenoCare.Domain;
using RenoCare.Domain.MetaData;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.Reports.Mediator.Queries
{
    /// <summary>
    /// Represents a get report request with specidfied properties with a corresponding response.
    /// </summary>
    public class GetReportQueryRequest : IRequest<ApiResponse<ReportDto>>
    {
        public int Id { get; set; }
    }

    /// <summary>
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
                                  .Include(x => x.Patient).ThenInclude(p => p.Viruses)
                                  .Select(x => new ReportDto
                                  {
                                      Id = x.Id,
                                      MedReqId = x.MedicationRequestId,
                                      DialysisUnitId = x.DialysisUnitId,
                                      Patient = new PatientDto
                                      {
                                          Id = x.Patient.Id,
                                          PatientName = x.Patient.User.FirstName + " " + x.Patient.User.LastName,
                                          BirthDate = x.Patient.BirthDate,
                                          Age = AgeFormatter.CalculateAge(x.Patient.BirthDate),
                                          Gender = x.Patient.Gender,
                                          Hypertension = x.Patient.HypertensionType.Name,
                                          Diabetes = x.Patient.DiabetesType.Name,
                                          Smoking = x.Patient.SmokingStatus.Name ?? "N/A",
                                          Viruses = string.Join(", ", x.Patient.Viruses.OrderBy(v => v.Id).Select(v => v.Abbreviation))
                                      },
                                      CreatedDate = x.CreatedDate,
                                      LastModifiedDate = x.LastModifiedDate,
                                      SessionDate = x.MedicationRequest.AppointmentDate,
                                      Nephrologist = x.Nephrologist,
                                      DialysisDuration = x.DialysisDuration,
                                      DialysisFrequency = x.DialysisFrequency,
                                      DialysisUnitName = x.DialysisUnit.Name,
                                      GeneralRemarks = x.GeneralRemarks,
                                      VascularAccessType = x.VascularAccessType,
                                      DialyzerType = x.DialyzerType,
                                      PreWeight = x.PreWeight,
                                      PostWeight = x.PostWeight,
                                      DryWeight = x.DryWeight,
                                      PreBloodPressure = x.PreBloodPressure,
                                      DuringBloodPressure = x.DuringBloodPressure,
                                      PostBloodPressure = x.DuringBloodPressure,
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
