using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Features.Reports.Dtos;
using RenoCare.Core.Features.Reports.Mediator.Queries;
using RenoCare.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.Reports.Mediator.Commands
{
    /// <summary>
    /// Represents a create report command with specidfied properties with a corresponding response.
    /// </summary>
    public class CreateReportCommandRequest : IRequest<ApiResponse<ReportDto>>
    {
        public ReportDto Report { get; set; }
    }

    /// <summary>
    /// Represents a handler for the create report command request.
    /// </summary>
    public class CreateReportCommandRequestHandler : ResponseHandler,
        IRequestHandler<CreateReportCommandRequest, ApiResponse<ReportDto>>
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Report> _reportsRepo;
        private readonly IRepository<MedicationRequest> _medicationRequestsRepo;

        #endregion

        #region Ctor

        public CreateReportCommandRequestHandler(IRepository<Report> reportsRepo,
            IRepository<MedicationRequest> medicationRequestsRepo,
            IHttpContextAccessor httpContextAccessor,
            IMediator mediator)
        {
            _reportsRepo = reportsRepo;
            _medicationRequestsRepo = medicationRequestsRepo;
            _httpContextAccessor = httpContextAccessor;
            _mediator = mediator;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the create report command.
        /// </summary>
        /// <param name="request">Mediator request</param>
        /// <param name="cancellationToken">Represents a notification to cancel the request.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the authentication response.
        /// </returns>
        public async Task<ApiResponse<ReportDto>> Handle(CreateReportCommandRequest request, CancellationToken cancellationToken)
        {
            int unitId;
            var ctxUnitId = _httpContextAccessor.HttpContext.Items["unitId"];

            if (!int.TryParse(ctxUnitId?.ToString(), out unitId))
                return BadRequest<ReportDto>();

            // allowed to create a report for it or not
            var medRequest = await _medicationRequestsRepo.Table.Include(x => x.Status)
                .FirstOrDefaultAsync(x => x.Id == request.Report.MedReqId && x.DialysisUnitId == unitId);

            if (medRequest == null)
                return Unauthorized<ReportDto>();

            if (medRequest.Status.Name != "Completed")
                return BadRequest<ReportDto>();

            //Calculate some report fields
            var report = new Report
            {
                MedicationRequest = medRequest,

                DialysisUnitId = unitId,
                PatientId = medRequest.PatientId,
                Nephrologist = request.Report.Nephrologist,
                DialysisDuration = request.Report.DialysisDuration,
                DialysisFrequency = request.Report.DialysisFrequency,
                VascularAccessType = request.Report.VascularAccessType,
                DialyzerType = request.Report.DialyzerType,
                GeneralRemarks = request.Report.GeneralRemarks,
                PreWeight = request.Report.PreWeight,
                PostWeight = request.Report.PostWeight,
                PreBloodPressure = request.Report.PreBloodPressure,
                DuringBloodPressure = request.Report.DuringBloodPressure,
                PostBloodPressure = request.Report.PostBloodPressure,
                HeartRate = request.Report.HeartRate,
                DryWeight = request.Report.DryWeight,
                PreUrea = request.Report.PreUrea,
                PostUrea = request.Report.PostUrea,
                TotalFluidRemoval = request.Report.TotalFluidRemoval,
                UrineOutput = request.Report.UrineOutput,
                UreaReductionRatio = request.Report.UreaReductionRatio,
                FluidRemovalRate = request.Report.FluidRemovalRate,
                Kt_V = request.Report.Kt_V,
                Creatinine = request.Report.Creatinine,
                Potassium = request.Report.Potassium,
                Hemoglobin = request.Report.Hemoglobin,
                Hematocrit = request.Report.Hematocrit,
                Albumin = request.Report.Albumin,

                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
            };

            await _reportsRepo.InsertAsync(report);
            await _reportsRepo.SaveAsync();

            medRequest.ReportId = report.Id;
            await _medicationRequestsRepo.SaveAsync();

            return await _mediator.Send(new GetReportQueryRequest { Id = report.Id });
        }

        #endregion
    }
}
