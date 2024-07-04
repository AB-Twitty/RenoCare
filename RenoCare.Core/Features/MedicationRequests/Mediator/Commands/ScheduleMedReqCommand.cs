using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Extensions;
using RenoCare.Domain;
using RenoCare.Domain.MetaData;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.MedicationRequests.Mediator.Commands
{
    /// <summary>
    /// Represents a request to schedule a medication request
    /// </summary>
    public class ScheduleMedReqCommandRequest : IRequest<ApiResponse<string>>
    {
        public int UnitId { get; set; }
        public DateTime Date { get; set; }
        public int SessionId { get; set; }
        public string PatientRemarks { get; set; }
        public int MedReqTypeId { get; set; }
        public TreatmentType Treatment { get; set; }
    }

    /// <summary>
    /// Represents a validator for the request to schedule a medication request
    /// </summary>
    public class ScheduleMedReqCommandRequstValidator : AbstractValidator<ScheduleMedReqCommandRequest>
    {
        private readonly IRepository<DialysisUnit> _unitRepo;
        private readonly IRepository<SessionTimetable> _sessionRepo;
        private readonly IRepository<MedicationRequestType> _medReqTypeRepo;

        public ScheduleMedReqCommandRequstValidator(IRepository<DialysisUnit> unitRepo, IRepository<SessionTimetable> sessionRepo, IRepository<MedicationRequestType> medReqTypeRepo)
        {
            _unitRepo = unitRepo;
            _sessionRepo = sessionRepo;
            _medReqTypeRepo = medReqTypeRepo;

            RuleFor(x => x.UnitId)
                .NotEmptyWithMessage()
                .MustAsync(async (id, _) => await _unitRepo.Table.AnyAsync(x => x.Id == id && !x.IsDeleted))
                .WithMessage(string.Format(Transcriptor.Response.EntityNotFound, "{PropertyValue}"));

            RuleFor(x => x.Date)
                .NotEmptyWithMessage()
                .Must(date => date >= DateTime.Now);

            RuleFor(x => x.SessionId)
                .NotEmptyWithMessage()
                .MustAsync(async (req, id, _) =>
                {
                    var session = await _sessionRepo.Table.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted
                        && x.DialysisUnitId == req.UnitId && req.Date.DayOfWeek == x.Day);

                    if (session == null)
                        return false;

                    return true;
                });

            RuleFor(x => x.MedReqTypeId)
                .NotEmptyWithMessage()
                .MustAsync(async (id, _) => await _medReqTypeRepo.Table.AnyAsync(x => x.Id == id && x.IsActive))
                .WithMessage(string.Format(Transcriptor.Response.EntityNotFound, "{PropertyValue}"));

            RuleFor(x => x.Treatment)
                .NotEmptyWithMessage()
                .IsInEnum()
                .MustAsync(async (req, treatment, _) =>
                {
                    var unit = await _unitRepo.Table.FirstOrDefaultAsync(x => x.Id == req.UnitId);

                    switch (treatment)
                    {
                        case TreatmentType.HD:
                            return unit.IsHdSupported;
                        case TreatmentType.HDF:
                            return unit.IsHdfSupported;
                        default:
                            return false;
                    }
                });

        }

    }

    /// <summary>
    /// Represents a handler for the request to schedule a medication request
    /// </summary>
    public class ScheduleMedReqCommandRequestHandler : ResponseHandler,
        IRequestHandler<ScheduleMedReqCommandRequest, ApiResponse<string>>
    {
        #region Fields

        private readonly IRepository<MedicationRequest> _medReqRepo;
        private readonly IRepository<Patient> _patientRepo;
        private readonly IHttpContextAccessor _ctxAccessor;

        #endregion

        #region Ctor

        public ScheduleMedReqCommandRequestHandler(IRepository<MedicationRequest> medReqRepo, IRepository<Patient> patientRepo, IHttpContextAccessor ctxAccessor)
        {
            _medReqRepo = medReqRepo;
            _patientRepo = patientRepo;
            _ctxAccessor = ctxAccessor;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the request to schedule a medication request.
        /// </summary>
        /// <param name="request">Mediator request</param>
        /// <param name="cancellationToken">Represents a notification to cancel the request.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains a value indicating whether the request was successfull or not.
        /// </returns>
        public async Task<ApiResponse<string>> Handle(ScheduleMedReqCommandRequest request, CancellationToken cancellationToken)
        {
            var curr_user = _ctxAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                ?.Value;

            var patient = await _patientRepo.Table.FirstOrDefaultAsync(x => x.UserId == curr_user);

            if (patient == null)
                return BadRequest<string>();

            var medReq = new MedicationRequest
            {
                PatientId = patient.Id,
                AppointmentDate = request.Date,
                DialysisUnitId = request.UnitId,
                PatientProblem = request.PatientRemarks,
                SessionId = request.SessionId,
                StatusId = 1,
                Treatment = request.Treatment,
                TypeId = request.MedReqTypeId
            };

            await _medReqRepo.InsertAsync(medReq);
            await _medReqRepo.SaveAsync();

            return Success(bool.TrueString);
        }

        #endregion
    }
}
