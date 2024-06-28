using FluentValidation;
using RenoCare.Core.Extensions;
using RenoCare.Core.Features.DialysisUnits.Dtos;
using System;

namespace RenoCare.Core.Features.DialysisUnits.Validators
{
    public class SessionTimeValidator : AbstractValidator<SessionTimeDto>
    {
        public SessionTimeValidator()
        {
            RuleFor(p => p.Time)
                .NotNullWithMessage();

            RuleFor(p => p.Day)
                .NotNullWithMessage()
                .Must(d => Enum.IsDefined(typeof(DayOfWeek), d))
                .WithMessage("Invalid day.");
        }
    }
}
