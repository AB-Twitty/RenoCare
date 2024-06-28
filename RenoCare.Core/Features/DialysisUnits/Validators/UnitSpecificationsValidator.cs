using FluentValidation;
using RenoCare.Core.Extensions;
using RenoCare.Core.Features.DialysisUnits.Dtos;

namespace RenoCare.Core.Features.DialysisUnits.Validators
{
    public class UnitSpecificationsValidator : AbstractValidator<UnitSpecificationsDto>
    {
        public UnitSpecificationsValidator()
        {
            RuleFor(p => p.Name)
                .NotNullWithMessage().NotEmptyWithMessage();

            RuleFor(p => p.Address)
                .NotNullWithMessage().NotEmptyWithMessage();

            RuleFor(p => p.Description)
                .NotNullWithMessage().NotEmptyWithMessage();

            RuleFor(p => p.Country)
                .NotNullWithMessage().NotEmptyWithMessage();

            RuleFor(p => p.City)
                .NotNullWithMessage().NotEmptyWithMessage();

            RuleFor(p => p.IsHdSupported)
                .NotNullWithMessage();

            RuleFor(p => p.HdPrice)
                .Must((model, price) => !model.IsHdSupported || price.HasValue)
                .WithMessage("A price for HD must be provided");

            RuleFor(p => p.IsHdfSupported)
                .NotNullWithMessage();

            RuleFor(p => p.HdfPrice)
                .Must((model, price) => !model.IsHdfSupported || price.HasValue)
                .WithMessage("A price for HDF must be provided");

            RuleFor(p => p.IsHdSupported || p.IsHdfSupported)
                .Must(isSupported => isSupported)
                .WithMessage("At least one of HD or HDF must be supported")
                .WithName("HdOrHdf");
        }
    }
}
