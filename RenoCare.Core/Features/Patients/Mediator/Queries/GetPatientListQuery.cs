using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Extensions;
using RenoCare.Core.Features.Patients.DTOs;
using RenoCare.Core.Helpers;
using RenoCare.Domain;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.Patients.Mediator.Queries
{
    /// <summary>
    /// Represents a get patient list request with specidfied properties with a corresponding response.
    /// </summary>
    public class GetPatientListQueryRequest : IRequest<ApiResponse<IPagedList<PatientListItemDto>>>
    {
        [FromQuery]
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = int.MaxValue;
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

                return await query.Include(p => p.User).Select(p => new PatientListItemDto
                {
                    Id = p.Id,
                    FirstName = p.User.FirstName,
                    LastName = p.User.LastName,
                    Diabetes = p.Diabetes,
                    Hypertension = p.Hypertension,
                    Gender = "Male",
                    ReportsCount = 42,
                    Age = now.Year - birth.Year
                }).ToPagedListAsync(request.PageIndex, request.PageSize);
            });

            return Success(list);
        }

        #endregion
    }
}
