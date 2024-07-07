using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Extensions;
using RenoCare.Core.Features.Patients.DTOs;
using RenoCare.Core.Helpers;
using RenoCare.Core.Helpers.Contracts;
using RenoCare.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.Patients.Mediator.Queries
{
    /// <summary>
    /// Represents a get patient list request with specidfied properties with a corresponding response.
    /// </summary>
    public class GetPatientListQueryRequest : IRequest<ApiResponse<IPagedList<PatientListItemDto>>>,
        ISearchable
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = int.MaxValue;

        public string SortColumn { get; set; } = "Id";
        public string SortDirection { get; set; } = "ASC";

        public IDictionary<string, string> SearchDict { get; set; }
    }

    // <summary>
    /// Represents a handler for the patients list query request.
    /// </summary>
    public class GetPatientListQueryRequestHandler : ResponseHandler,
        IRequestHandler<GetPatientListQueryRequest, ApiResponse<IPagedList<PatientListItemDto>>>
    {
        #region Fields

        private readonly IRepository<Patient> _patientRepo;
        private readonly IRepository<MedicationRequest> _medReqRepo;
        private readonly IRepository<Domain.MedicationRequestStatus> _medStatusRepo;
        private readonly IHttpContextAccessor _ctxAccessor;

        #endregion

        #region Ctor

        public GetPatientListQueryRequestHandler(IRepository<Patient> patientRepo,
            IRepository<MedicationRequest> medReqRepo, IHttpContextAccessor ctxAccessor,
            IRepository<Domain.MedicationRequestStatus> medStatusRepo)
        {
            _patientRepo = patientRepo;
            _medReqRepo = medReqRepo;
            _ctxAccessor = ctxAccessor;
            _medStatusRepo = medStatusRepo;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the patients list request.
        /// </summary>
        /// <param name="request">Mediator request</param>
        /// <param name="cancellationToken">Represents a notification to cancel the request.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the authentication response.
        /// </returns>
        public async Task<ApiResponse<IPagedList<PatientListItemDto>>> Handle(GetPatientListQueryRequest request,
            CancellationToken cancellationToken)
        {
            IPagedList<PatientListItemDto> list = new PagedList<PatientListItemDto>();

            var statuses = await _medStatusRepo.GetAllAsync(q => q.OrderBy(s => s.Id));

            var statuses_names = statuses.Select(x => x.Name);

            if (_ctxAccessor.HttpContext.User.IsInRole("HealthCare"))
            {
                int.TryParse(_ctxAccessor.HttpContext.Items["unitId"].ToString(), out int unitId);

                var patientIdsQuery = _medReqRepo.Table
                                 .Where(x => x.DialysisUnitId == unitId)
                                 .Select(x => x.Patient.Id)
                                 .Distinct();

                var pagedPatientIds = (await patientIdsQuery.ToPagedListAsync(request.PageIndex, request.PageSize)).Items;

                list = await _patientRepo.ApplyQueryAsync(async q =>
                {
                    var qry = q.Where(x => pagedPatientIds.Contains(x.Id))
                         .Include(p => p.User).Include(p => p.Reports).Include(p => p.Viruses)
                         .Select(p => new PatientListItemDto
                         {
                             Id = p.Id,
                             PatientName = p.User.FirstName + " " + p.User.LastName,
                             ReportsSameUnit = p.Reports.Where(x => x.DialysisUnitId == unitId).Count(),
                             ReportsOverral = p.Reports.Count(),
                             Diabetes = p.DiabetesType.Name,
                             Hypertension = p.HypertensionType.Name,
                             Gender = p.Gender.ToString(),
                             BirthDate = p.BirthDate,
                             Age = AgeFormatter.CalculateAge(p.BirthDate),
                             Smoking = p.SmokingStatus.Name ?? "--",
                             Viruses = string.Join(", ", p.Viruses.OrderBy(x => x.Id).Select(x => x.Abbreviation))
                         });

                    var totalCount = qry.Count();

                    qry = qry.FilterQuery(request);

                    var sorting_by_medStatus = statuses_names.Contains(request.SortColumn);

                    if (!sorting_by_medStatus && !string.IsNullOrEmpty(request.SortColumn) && !string.IsNullOrEmpty(request.SortDirection))
                        qry = qry.OrderBy($"{request.SortColumn} {request.SortDirection}");

                    var paged_list = await qry.ToPagedListAsync(request.PageIndex, request.PageSize, totalCount, 1, qry.Count());

                    foreach (var patient in paged_list.Items)
                    {
                        patient.MedReqCnts = await _medReqRepo.ApplyQueryAsync(async q =>
                        {
                            var medReqs = await q.Include(x => x.Status)
                                .Where(m => m.PatientId == patient.Id)
                                .ToListAsync();

                            return medReqs.Where(x => x.Status != null)
                                .GroupBy(m => m.Status.Name)
                                .Where(g => statuses_names.Contains(g.Key))
                                .ToDictionary(g => g.Key, g => g.Count());
                        });
                    }


                    if (sorting_by_medStatus)
                    {
                        string sortExpression = $"MedReqCnts.Where(Key == \"{request.SortColumn}\").Select(Value).DefaultIfEmpty(0).FirstOrDefault() {request.SortDirection}";

                        paged_list.Items = paged_list.Items.AsQueryable().OrderBy(sortExpression).ToList();
                    }

                    return paged_list;
                });
            }

            else if (_ctxAccessor.HttpContext.User.IsInRole("Admin"))
            {
                list = await _patientRepo.ApplyQueryAsync(async query =>
                {
                    var qry = query
                        .Include(p => p.User).Include(p => p.Reports).Include(p => p.Viruses)
                        .Select(p => new PatientListItemDto
                        {
                            Id = p.Id,
                            PatientName = p.User.FirstName + " " + p.User.LastName,
                            ReportsOverral = p.Reports.Count(),
                            Diabetes = p.DiabetesType.Name,
                            Hypertension = p.HypertensionType.Name,
                            Gender = p.Gender.ToString(),
                            BirthDate = p.BirthDate,
                            Age = AgeFormatter.CalculateAge(p.BirthDate),
                            Smoking = p.SmokingStatus.Name ?? "--",
                            Viruses = string.Join(", ", p.Viruses.OrderBy(x => x.Id).Select(x => x.Abbreviation))
                        });

                    var totalCount = qry.Count();

                    qry = qry.FilterQuery(request);

                    var sorting_by_medStatus = statuses_names.Contains(request.SortColumn);

                    if (!sorting_by_medStatus && !string.IsNullOrEmpty(request.SortColumn) && !string.IsNullOrEmpty(request.SortDirection))
                        qry = qry.OrderBy($"{request.SortColumn} {request.SortDirection}");

                    var paged_list = await qry.ToPagedListAsync(request.PageIndex, request.PageSize, totalCount, 1, qry.Count());

                    foreach (var patient in paged_list.Items)
                    {
                        patient.MedReqCnts = await _medReqRepo.ApplyQueryAsync(async q =>
                        {
                            var medReqs = await q.Include(x => x.Status)
                                .Where(m => m.PatientId == patient.Id)
                                .ToListAsync();

                            return medReqs.Where(x => x.Status != null)
                                .GroupBy(m => m.Status.Name)
                                .Where(g => statuses_names.Contains(g.Key))
                                .ToDictionary(g => g.Key, g => g.Count());
                        });
                    }


                    if (sorting_by_medStatus)
                    {
                        string sortExpression = $"MedReqCnts.Where(Key == \"{request.SortColumn}\").Select(Value).DefaultIfEmpty(0).FirstOrDefault() {request.SortDirection}";

                        paged_list.Items = paged_list.Items.AsQueryable().OrderBy(sortExpression).ToList();
                    }

                    return paged_list;
                });
            }

            return Success(list);
        }

        #endregion
    }
}
