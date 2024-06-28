using FluentValidation;
using FluentValidation.Results;
using MediatR;
using RenoCare.Core.Base;
using RenoCare.Core.Extensions;
using RenoCare.Core.Features.Authentication.Contracts;
using RenoCare.Core.Features.Authentication.Contracts.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.Authentication.Mediator.Commands
{
    /// <summary>
    /// Represents a request to set first time password using OTP code.
    /// </summary>
    public class SetPasswordWithOtpCommandRequest : IRequest<ApiResponse<AuthResponse>>
    {
        public OtpPasswordSetRequest PasswordModel { get; set; }
    }

    /// <summary>
    /// A validator for password set with otp command.
    /// </summary>
    public class SetPasswordWithOtpCommandValidator : AbstractValidator<SetPasswordWithOtpCommandRequest>
    {
        private readonly IAuthService _authService;
        public SetPasswordWithOtpCommandValidator(IAuthService authService)
        {
            _authService = authService;

            RuleFor(p => p.PasswordModel.Email)
                .NotNullWithMessage().NotEmptyWithMessage()
                .EmailAddress();

            RuleFor(P => P.PasswordModel.Password)
                .NotNullWithMessage().NotEmptyWithMessage();

            RuleFor(p => p.PasswordModel.OTP)
                .NotNullWithMessage().NotEmptyWithMessage()
                .Must(otp =>
                {
                    return (otp?.Length == 6 && int.TryParse(otp, out int _));
                }).WithMessage("Invalid OTP code.");
        }
    }

    /// <summary>
    /// Represents a handler for the request to validate otp to set password.
    /// </summary>
    public class SetPasswordWithOtpCommandRequestHandler : ResponseHandler,
        IRequestHandler<SetPasswordWithOtpCommandRequest, ApiResponse<AuthResponse>>
    {
        #region Fields

        private readonly IAuthService _authService;

        #endregion

        #region Ctor

        public SetPasswordWithOtpCommandRequestHandler(IAuthService authService)
        {
            _authService = authService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the validation for otp.
        /// </summary>
        /// <param name="request">mediator request</param>
        /// <param name="cancellationToken">Represents a notification to cancel the request.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task contains the authentication response.
        /// </returns>
        public async Task<ApiResponse<AuthResponse>> Handle(SetPasswordWithOtpCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _authService.HasPasswordAsync(request.PasswordModel.Email);
                if (result)
                    return BadRequest<AuthResponse>("User already has password.");
            }
            catch
            {
                return BadRequest<AuthResponse>("This E-mail address is not regestered.");
            }

            if (!await _authService.ValidateOtpAsync(request.PasswordModel.Email, request.PasswordModel.OTP))
                return BadRequest<AuthResponse>("Invalid OTP Login Attempt.");

            (bool Succeeded, IEnumerable<ValidationFailure> errors) = await _authService.AddPasswordAsync(request.PasswordModel.Email, request.PasswordModel.Password);

            if (Succeeded)
            {
                var response = await _authService.AuthenticateAsync(new AuthRequest
                {
                    Email = request.PasswordModel.Email,
                    Password = request.PasswordModel.Password,
                    RememberMe = false
                });

                return Success(response);
            }

            throw new ValidationException(errors.FirstOrDefault()?.ErrorMessage ?? "", errors);
        }

        #endregion
    }
}
