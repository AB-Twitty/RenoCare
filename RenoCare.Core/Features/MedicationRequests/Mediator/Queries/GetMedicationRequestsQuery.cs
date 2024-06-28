using MediatR;
using Microsoft.AspNetCore.Http;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Extensions;
using RenoCare.Core.Features.MedicationRequests.DTOs;
using RenoCare.Core.Helpers;
using RenoCare.Core.Helpers.Contracts;
using RenoCare.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.MedicationRequests.Mediator.Queries
{
    /// <summary>
    /// Represents a get medication requests list request with specidfied properties with a corresponding response.
    /// </summary>
    public class GetMedicationRequestsQueryRequest : IRequest<ApiResponse<IPagedList<MedicationRequestListItemDto>>>,
        ISearchable
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = int.MaxValue;

        public string SortColumn { get; set; } = "Id";
        public string SortDirection { get; set; } = "ASC";

        public IDictionary<string, string> SearchDict { get; set; }
    }

    // <summary>
    /// Represents a handler for the medication requests list query request.
    /// </summary>
    public class GetMedicationRequestsQueryRequestHandler : ResponseHandler,
        IRequestHandler<GetMedicationRequestsQueryRequest, ApiResponse<IPagedList<MedicationRequestListItemDto>>>
    {
        #region Fields

        private readonly IRepository<MedicationRequest> _medicationRequestRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Ctor

        public GetMedicationRequestsQueryRequestHandler(IRepository<MedicationRequest> medicationRequestRepo
            , IHttpContextAccessor httpContextAccessor)
        {
            _medicationRequestRepo = medicationRequestRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the medication requests list request.
        /// </summary>
        /// <param name="request">Mediator request</param>
        /// <param name="cancellationToken">Represents a notification to cancel the request.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the authentication response.
        /// </returns>
        public async Task<ApiResponse<IPagedList<MedicationRequestListItemDto>>> Handle(GetMedicationRequestsQueryRequest request, CancellationToken cancellationToken)
        {
            var list = await _medicationRequestRepo.ApplyQueryAsync(async query =>
            {
                if (_httpContextAccessor.HttpContext.User.IsInRole("HealthCare"))
                {
                    var unitId = _httpContextAccessor.HttpContext.Items["unitId"];
                    var unitName = _httpContextAccessor.HttpContext.Items["unit"];

                    if (unitId == null || unitName == null)
                        return new PagedList<MedicationRequestListItemDto>();

                    query = query.Where(x => x.DialysisUnitId == int.Parse(unitId.ToString()));
                }



                var qry = query
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
                    });

                var totalCount = qry.Count();

                qry = qry.FilterQuery(request);

                if (!string.IsNullOrEmpty(request.SortColumn) && !string.IsNullOrEmpty(request.SortDirection))
                    qry = qry.OrderBy($"{request.SortColumn} {request.SortDirection}");

                return await qry.ToPagedListAsync(request.PageIndex, request.PageSize, totalCount);
            });

            return Success(list);
        }

        #endregion
    }
}
