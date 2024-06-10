using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Features.Reports.Dtos;
using RenoCare.Core.Features.Reports.Mediator.Queries;
using RenoCare.Domain;
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
            var requestId = 1;

            int unitId;
            var ctxUnitId = _httpContextAccessor.HttpContext.Items["unitId"];

            if (!int.TryParse(ctxUnitId?.ToString(), out unitId))
                return BadRequest<ReportDto>();

            // allowed to create a report for it or not
            var medRequest = await _medicationRequestsRepo.Table.Include(x => x.Status)
                .FirstOrDefaultAsync(x => x.Id == requestId && x.DialysisUnitId == unitId);

            if (medRequest == null)
                return Unauthorized<ReportDto>();

            if (medRequest.Status.Name != "Completed")
                return BadRequest<ReportDto>();

            //Calculate some report fields

            double urr = 50.3;
            double rate = 60;
            double kt = 1.3;

            var report = new Report
            {
                DialysisUnitId = unitId,
                MedicationRequestId = medRequest.Id,
                PatientId = medRequest.PatientId,
                Nephrologist = request.Report.Nephrologist,
                DialysisDuration = request.Report.DialysisDuration,
                DialysisFrequency = request.Report.DialysisFrequency,
                VascularAccessType = request.Report.VascularAccessType,
                DialyzerType = request.Report.DialyzerType,
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
                UreaReductionRatio = urr,
                FluidRemovalRate = rate,
                Kt_V = kt,
                Creatinine = request.Report.Creatinine,
                Potassium = request.Report.Potassium,
                Hemoglobin = request.Report.Hemoglobin,
                Hematocrit = request.Report.Hematocrit,
                Albumin = request.Report.Albumin
            };


            await _reportsRepo.InsertAsync(report);
            await _reportsRepo.SaveAsync();

            return await _mediator.Send(new GetReportQueryRequest { Id = report.Id });
        }

        #endregion
    }
}
