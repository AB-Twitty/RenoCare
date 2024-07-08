using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Extensions;
using RenoCare.Core.Features.Patients.DTOs;
using RenoCare.Core.Features.Reports.Dtos;
using RenoCare.Core.Helpers;
using RenoCare.Core.Helpers.Contracts;
using RenoCare.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.Reports.Mediator.Queries
{
    /// <summary>
    /// Represents a request to get a list of medical reports
    /// </summary>
    public class GetReportsListQueryRequest : IRequest<ApiResponse<IPagedList<ReportDto>>>, ISearchable
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = int.MaxValue;

        public string SortColumn { get; set; } = "Id";
        public string SortDirection { get; set; } = "ASC";

        public IDictionary<string, string> SearchDict { get; set; }
    }

    /// <summary>
    /// Represents for the request to get a list of medical reports
    /// </summary>
    public class GetReportsListQueryRequestHandler : ResponseHandler,
        IRequestHandler<GetReportsListQueryRequest, ApiResponse<IPagedList<ReportDto>>>
    {
        #region Fields

        private readonly IRepository<Report> _reportRepo;
        private readonly IHttpContextAccessor _ctxAccessor;

        #endregion

        #region Ctor

        public GetReportsListQueryRequestHandler(IRepository<Report> reportRepo, IHttpContextAccessor ctxAccessor)
        {
            _reportRepo = reportRepo;
            _ctxAccessor = ctxAccessor;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the request to get a list of medical reports.
        /// </summary>
        /// <param name="request">Mediator request</param>
        /// <param name="cancellationToken">Represents a notification to cancel the request.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains a paged list of medical reports.
        /// </returns>
        public async Task<ApiResponse<IPagedList<ReportDto>>> Handle(GetReportsListQueryRequest request, CancellationToken cancellationToken)
        {
            var list = await _reportRepo.ApplyQueryAsync(async qry =>
            {
                if (_ctxAccessor.HttpContext.User.IsInRole("HealthCare"))
                {
                    if (!int.TryParse(_ctxAccessor.HttpContext.Items["unitId"]?.ToString(), out int unitId))
                        return new PagedList<ReportDto>();

                    qry = qry.Where(x => x.DialysisUnitId == unitId);
                }

                var query = qry.Include(x => x.Patient).ThenInclude(p => p.Viruses)
                                .Include(x => x.MedicationRequest).ThenInclude(x => x.Session)
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
                                    SessionTime = new DateTime(x.MedicationRequest.Session.Time.Ticks),
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

                                });

                var totalCount = query.Count();


                if (request.SearchDict.ContainsKey("Patient.PatientName"))
                {
                    var name = request.SearchDict["Patient.PatientName"].ToLower();
                    query = query.Where(x => x.Patient.PatientName.ToLower().Contains(name));
                    request.SearchDict.Remove("Patient.PatientName");
                }

                if (request.SearchDict.ContainsKey("Patient.Id") && int.TryParse(request.SearchDict["Patient.Id"].ToString(), out int id))
                {
                    query = query.Where(x => x.Patient.Id == id);
                    request.SearchDict.Remove("Patient.Id");
                }


                query = query.FilterQuery(request);

                if (!string.IsNullOrEmpty(request.SortColumn) && !string.IsNullOrEmpty(request.SortDirection))
                    query = query.OrderBy($"{request.SortColumn} {request.SortDirection}");

                return await query.ToPagedListAsync(request.PageIndex, request.PageSize, totalCount, 1, query.Count());
            });

            return Success(list);
        }

        #endregion
    }
}
