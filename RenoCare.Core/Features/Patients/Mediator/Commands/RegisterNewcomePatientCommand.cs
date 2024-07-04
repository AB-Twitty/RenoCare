using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Extensions;
using RenoCare.Core.Features.Authentication.Contracts;
using RenoCare.Core.Features.Authentication.Contracts.Models;
using RenoCare.Domain;
using RenoCare.Domain.Identity;
using RenoCare.Domain.MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.Patients.Mediator.Commands
{
    /// <summary>
    /// Represents a request to create a new patient
    /// </summary>
    public class RegisterNewcomePatientCommandRequest : IRequest<ApiResponse<AuthResponse>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public int DiabetesType { get; set; }
        public int HypertensionType { get; set; }
        public IList<int> Viruses { get; set; }
        public int SmokingStatus { get; set; }
        public string KidneyFailureCause { get; set; }
    }

    /// <summary>
    /// Represents a validator for the request to create a new patient
    /// </summary>
    public class RegisterNewcomePatientCommandRequestValidator : AbstractValidator<RegisterNewcomePatientCommandRequest>
    {

        private readonly IRepository<DiabetesType> _diabetesRepo;
        private readonly IRepository<HypertensionType> _hypertensionRepo;
        private readonly IRepository<SmokingStatus> _smokingRepo;
        private readonly IRepository<Virus> _virusRepo;
        private readonly IAuthService _authService;

        public RegisterNewcomePatientCommandRequestValidator(IRepository<DiabetesType> diabetesRepo, IRepository<HypertensionType> hypertensionRepo, IRepository<SmokingStatus> smokingRepo, IRepository<Virus> virusRepo, IAuthService authService)
        {
            _diabetesRepo = diabetesRepo;
            _hypertensionRepo = hypertensionRepo;
            _smokingRepo = smokingRepo;
            _virusRepo = virusRepo;

            RuleFor(x => x.FirstName)
                .NotNullWithMessage().NotEmptyWithMessage();

            RuleFor(x => x.LastName)
                .NotNullWithMessage().NotEmptyWithMessage();

            RuleFor(x => x.Email)
                .NotNullWithMessage().NotEmptyWithMessage()
                .EmailAddress()
                .MustAsync(async (email, _) =>
                {
                    return !await _authService.IsUserWithEmailExistsAsync(email);
                }).WithMessage("This E-mail Address Already Exists.");

            RuleFor(x => x.Password)
                .NotNullWithMessage().NotEmptyWithMessage();

            RuleFor(x => x.DiabetesType).Cascade(CascadeMode.Stop)
                .NotNullWithMessage().NotEmptyWithMessage()
                .MustAsync(async (id, _) => await _diabetesRepo.Table.AnyAsync(x => x.Id == id))
                .WithMessage(string.Format(Transcriptor.Response.EntityNotFound, "{PropertyValue}"));

            RuleFor(x => x.HypertensionType)
                .NotNullWithMessage().NotEmptyWithMessage()
                .MustAsync(async (id, _) => await _hypertensionRepo.Table.AnyAsync(x => x.Id == id))
                .WithMessage(string.Format(Transcriptor.Response.EntityNotFound, "{PropertyValue}"));

            RuleFor(x => x.Gender)
                .NotNullWithMessage().NotEmptyWithMessage()
                .IsInEnum();

            RuleFor(x => x.BirthDate)
                .NotNullWithMessage().NotEmptyWithMessage()
                .Must(date => date != null && date <= DateTime.Now.AddYears(-4));

            RuleFor(x => x.SmokingStatus)
                .NotNullWithMessage().NotEmptyWithMessage()
                .MustAsync(async (id, _) => await _smokingRepo.Table.AnyAsync(x => x.Id == id))
                .WithMessage(string.Format(Transcriptor.Response.EntityNotFound, "{PropertyValue}"));

            RuleFor(x => x.Viruses)
                .MustAsync(async (viruses, _) =>
                {
                    if (viruses == null || viruses.Count == 0)
                        return true;

                    foreach (var id in viruses)
                        if (!await _virusRepo.Table.AnyAsync(x => x.Id == id)) return false;

                    return true;
                })
                .WithMessage("Invalid ids");
            _authService = authService;
        }
    }

    /// <summary>
    /// Represents a handler for the request to create a new patient
    /// </summary>
    public class RegisterNewcomePatientCommandRequestHandler : ResponseHandler,
        IRequestHandler<RegisterNewcomePatientCommandRequest, ApiResponse<AuthResponse>>
    {
        #region Fields

        private readonly IRepository<Patient> _patientRepo;
        private readonly IRepository<Virus> _virusRepo;
        private readonly IAuthService _authService;

        #endregion

        #region Ctor

        public RegisterNewcomePatientCommandRequestHandler(IRepository<Patient> patientRepo,
            IAuthService authService,
            IRepository<Virus> virusRepo)
        {
            _patientRepo = patientRepo;
            _authService = authService;
            _virusRepo = virusRepo;
        }

        #endregion

        #region Methods

        public async Task<ApiResponse<AuthResponse>> Handle(RegisterNewcomePatientCommandRequest request, CancellationToken cancellationToken)
        {
            var user = new AppUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email
            };

            (bool Succeeded, IEnumerable<ValidationFailure> errors) = await _authService.AddNewUserAsync(user, request.Password, "Patient");

            if (!Succeeded)
            {
                throw new ValidationException(errors.FirstOrDefault()?.ErrorMessage ?? "", errors);
            }

            try
            {
                var viruses = await _virusRepo.GetAllAsync(q => q.Where(x => request.Viruses.Any(v => v == x.Id)));

                await _patientRepo.InsertAsync(new Patient
                {
                    UserId = user.Id,
                    DiabetesTypeId = request.DiabetesType,
                    HypertensionTypeId = request.HypertensionType,
                    Viruses = viruses,
                    SmokingStatusId = request.SmokingStatus,
                    Gender = request.Gender,
                    BirthDate = request.BirthDate,
                    KidneyFailureCause = request.KidneyFailureCause
                });

                await _patientRepo.SaveAsync();
            }
            catch (Exception ex)
            {
                await _authService.DeleteUserAsync(user);

                throw ex;
            }

            var response = await _authService.AuthenticateAsync(new AuthRequest
            {
                Email = request.Email,
                Password = request.Password,
                RememberMe = false
            });

            return Success(response);
        }

        #endregion
    }
}
