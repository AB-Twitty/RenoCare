using FluentValidation;
using FluentValidation.Validators;
using RenoCare.Domain.MetaData;

namespace RenoCare.Core.Extensions
{
    /// <summary>
    /// Represents abstract validator extensions for rule builder.
    /// </summary>
    public static class ValidatorExtensions
    {
        #region Utils

        private static bool BeValidBloodPressureFormat(string preBloodPressure)
        {
            // Check that the format of the blood pressure is "integers + '/' + integers"
            var parts = preBloodPressure?.Split('/');
            return parts?.Length == 2 && int.TryParse(parts[0], out _) && int.TryParse(parts[1], out _);
        }

        private static bool BeValidSystolicRange(string preBloodPressure)
        {
            var systolicString = preBloodPressure?.Split('/')[0];
            if (int.TryParse(systolicString, out var systolic))
            {
                return systolic >= 0 && systolic <= 180;
            }
            return false;
        }

        private static bool BeValidDiastolicRange(string preBloodPressure)
        {
            var parts = preBloodPressure?.Split('/');
            if (parts?.Length == 2 && int.TryParse(parts[0], out var systolic) && int.TryParse(parts[1], out var diastolic))
            {
                return diastolic >= 0 && diastolic <= 120 && diastolic < systolic;
            }
            return false;
        }

        #endregion


        /// <summary>
        /// Defines a 'not null' validator on the current rule builder with default error message.
        /// Validation will fail if the property is null.
        /// </summary>
        /// <typeparam name="T">Type of object being validated</typeparam>
        /// <typeparam name="TProperty">Type of property being validated</typeparam>
        /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
        /// <returns>A rule builder.</returns>
        public static IRuleBuilderOptions<T, TProperty> NotNullWithMessage<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new NotNullValidator<T, TProperty>()).WithMessage(Transcriptor.Validations.NotNull);
        }

        /// <summary>
        /// Defines a 'not null' validator on the current rule builder with default error message.
        /// Validation will fail if the property is null.
        /// </summary>
        /// <typeparam name="T">Type of object being validated</typeparam>
        /// <typeparam name="TProperty">Type of property being validated</typeparam>
        /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
        /// <returns>A rule builder.</returns>
        public static IRuleBuilderOptions<T, TProperty> NotEmptyWithMessage<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new NotEmptyValidator<T, TProperty>()).WithMessage(Transcriptor.Validations.NotNull);
        }

        /// <summary>
        /// Defines a 'blood pressure' validator on the current rule builder with default error message.
        /// Validation will fail if the plood pressure is invalid.
        /// </summary>
        /// <typeparam name="T">Type of object being validated</typeparam>
        /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
        /// <returns>A rule builder.</returns>
        public static IRuleBuilderOptions<T, string> ValidateBloodPressureWithMessages<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Must(BeValidBloodPressureFormat)
                .WithMessage("Invalid 'Blood Pressure' format.")
                .Must(BeValidSystolicRange)
                .WithMessage("'Systolic Blood Pressure' should be within the valid range.")
                .Must(BeValidDiastolicRange)
                .WithMessage("'Diastolic Blood Pressure' should be within the valid range.");
        }

    }
}
