using FluentValidation;
using RenoCare.Core.Extensions;
using RenoCare.Core.Features.Authentication.Mediator.Commands;

namespace RenoCare.Core.Features.Authentication.Validators
{
    /// <summary>
    /// Represents Login request validator.
    /// </summary>
    public class LoginCommandValidator : AbstractValidator<LoginCommandRequest>
    {
        public LoginCommandValidator()
        {
            RuleFor(p => p.Email)
                .NotNullWithMessage()
                .NotEmptyWithMessage()
                .EmailAddress();

            RuleFor(p => p.Password)
                .NotNullWithMessage()
                .NotEmptyWithMessage();

            RuleFor(p => p.RememberMe)
                .NotNullWithMessage();
        }
    }
}
