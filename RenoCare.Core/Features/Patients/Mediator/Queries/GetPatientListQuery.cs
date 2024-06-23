using MediatR;
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

        #endregion

        #region Ctor

        public GetPatientListQueryRequestHandler(IRepository<Patient> patientRepo)
        {
            _patientRepo = patientRepo;
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
            var list = await _patientRepo.ApplyQueryAsync(async query =>
            {
                DateTime now = DateTime.Now;
                DateTime birth = DateTime.Now.AddYears(-32);

                var qry = query
                    .Include(p => p.User)
                    .Select(p => new PatientListItemDto
                    {
                        Id = p.Id,
                        PatientName = p.User.FirstName + " " + p.User.LastName,
                        ReportsSameUnit = 12,
                        ReportsOverral = 42,
                        Diabetes = p.DiabetesType.Name,
                        Hypertension = p.HypertensionType.Name,
                        Gender = "Male",
                        Age = now.Year - birth.Year,
                        Smoking = p.SmokingStatus.Name ?? "--"
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
