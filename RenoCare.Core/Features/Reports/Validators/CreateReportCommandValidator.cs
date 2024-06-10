using FluentValidation;
using RenoCare.Core.Extensions;
using RenoCare.Core.Features.Reports.Mediator.Commands;
using RenoCare.Domain;
using System;

namespace RenoCare.Core.Features.Reports.Validators
{
    /// <summary>
    /// Represents create report command validator.
    /// </summary>
    public class CreateReportCommandValidator : AbstractValidator<CreateReportCommandRequest>
    {
        public CreateReportCommandValidator()
        {
            RuleFor(x => x.Report.Nephrologist)
                .NotNullWithMessage().NotEmptyWithMessage()
                .MaximumLength(80);

            RuleFor(x => x.Report.DialysisDuration)
                .NotNullWithMessage().NotEmptyWithMessage()
                .InclusiveBetween(1.0, 8.0);

            RuleFor(x => x.Report.DialysisFrequency)
                .NotNullWithMessage().NotEmptyWithMessage()
                .InclusiveBetween(1, 7);

            RuleFor(x => x.Report.VascularAccessType)
                .NotNullWithMessage().NotEmptyWithMessage()
                .Must(value => value != null && Enum.TryParse(typeof(VascularType), value.ToString(), out _))
                .WithMessage("Invalid vascular access type.");

            RuleFor(x => x.Report.DialyzerType)
                .NotNullWithMessage().NotEmptyWithMessage()
                .Must(value => value != null && Enum.TryParse(typeof(DialyzerType), value.ToString(), out _))
                .WithMessage("Invalid dialyzer type.");



            RuleFor(x => x.Report.PreWeight)
                .NotNullWithMessage().NotEmptyWithMessage()
                .InclusiveBetween(10.0, 200.0);

            RuleFor(x => x.Report.PostWeight)
            .NotNullWithMessage().NotEmptyWithMessage()
            .InclusiveBetween(10.0, 200.0)
            .Must((command, postWeight) => postWeight < command.Report.PreWeight)
            .WithMessage("'Post-weight' must be less than 'pre-weight'.");

            RuleFor(x => x.Report.DryWeight)
                .NotNullWithMessage().NotEmptyWithMessage()
                .InclusiveBetween(10.0, 200.0);

            RuleFor(x => x.Report.HeartRate)
                .NotNullWithMessage().NotEmptyWithMessage()
                .InclusiveBetween(40, 120);

            RuleFor(x => x.Report.PreBloodPressure)
                .NotNullWithMessage().NotEmptyWithMessage()
                .ValidateBloodPressureWithMessages();

            RuleFor(x => x.Report.DuringBloodPressure)
                .NotNullWithMessage().NotEmptyWithMessage()
                .ValidateBloodPressureWithMessages();

            RuleFor(x => x.Report.PostBloodPressure)
                .NotNullWithMessage().NotEmptyWithMessage()
                .ValidateBloodPressureWithMessages();


            RuleFor(x => x.Report.PreUrea)
                .NotNullWithMessage().NotEmptyWithMessage()
                .InclusiveBetween(5.0, 200.0);

            RuleFor(x => x.Report.PostUrea)
                .NotNullWithMessage().NotEmptyWithMessage()
                .InclusiveBetween(5.0, 200.0);

            RuleFor(x => x.Report.TotalFluidRemoval)
                .NotNullWithMessage().NotEmptyWithMessage()
                .InclusiveBetween(0.0, 5000.0);

            RuleFor(x => x.Report.UrineOutput)
                .NotNullWithMessage().NotEmptyWithMessage()
                .InclusiveBetween(0.0, 5000.0);


            RuleFor(x => x.Report.Creatinine)
                .NotNullWithMessage().NotEmptyWithMessage()
                .InclusiveBetween(0.5, 15.0);

            RuleFor(x => x.Report.Potassium)
                .NotNullWithMessage().NotEmptyWithMessage()
                .InclusiveBetween(2.5, 6.5);

            RuleFor(x => x.Report.Hemoglobin)
                .NotNullWithMessage().NotEmptyWithMessage()
                .InclusiveBetween(5.0, 20.0);

            RuleFor(x => x.Report.Hematocrit)
                .NotNullWithMessage().NotEmptyWithMessage()
                .InclusiveBetween(15.0, 60.0);

            RuleFor(x => x.Report.Albumin)
                .NotNullWithMessage().NotEmptyWithMessage()
                .InclusiveBetween(2.0, 6.0);
        }

    }
}
