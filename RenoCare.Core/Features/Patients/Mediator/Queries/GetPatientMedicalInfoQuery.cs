using MediatR;
using Microsoft.EntityFrameworkCore;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Features.Patients.DTOs;
using RenoCare.Domain;
using RenoCare.Domain.MetaData;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.Patients.Mediator.Queries
{
    /// <summary>
    /// Represents a get patient medical Info request with specidfied properties with a corresponding response.
    /// </summary>
    public class GetPatientMedicalInfoQueryRequest : IRequest<ApiResponse<PatientDto>>
    {
        public int Id { get; set; }
    }

    // <summary>
    /// Represents a handler for the patients medical info query request.
    /// </summary>
    public class GetPatientMedicalInfoQueryRequestHandler : ResponseHandler,
        IRequestHandler<GetPatientMedicalInfoQueryRequest, ApiResponse<PatientDto>>
    {
        #region Fields

        private readonly IRepository<Patient> _patientRepo;

        #endregion

        #region Ctor

        public GetPatientMedicalInfoQueryRequestHandler(IRepository<Patient> patientRepo)
        {
            _patientRepo = patientRepo;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the patients medical info request.
        /// </summary>
        /// <param name="request">Mediator request</param>
        /// <param name="cancellationToken">Represents a notification to cancel the request.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the authentication response.
        /// </returns>
        public async Task<ApiResponse<PatientDto>> Handle(GetPatientMedicalInfoQueryRequest request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepo.ApplyQueryAsync(async query =>
            {
                return await query.Where(x => x.Id == request.Id)
                                  .Select(x => new PatientDto
                                  {
                                      Id = x.Id,
                                      PatientName = x.User.FirstName + " " + x.User.LastName,
                                      Hypertension = x.HypertensionType.Name,
                                      Diabetes = x.DiabetesType.Name,
                                      BirthDate = DateTime.Now.AddHours(-24),
                                      Age = 24,
                                      Gender = "Male",
                                      Smoking = x.SmokingStatus.Name
                                  }).FirstOrDefaultAsync();
            });

            if (patient == null)
                return NotFound<PatientDto>(string.Format(Transcriptor.Response.EntityNotFound, request.Id));

            return Success(patient);
        }

        #endregion
    }
}
