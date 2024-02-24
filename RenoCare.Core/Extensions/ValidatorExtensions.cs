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
            return ruleBuilder.SetValidator(new NotEmptyValidator<T, TProperty>()).WithMessage(Transcriptor.Validations.NotEmpty);
        }
    }
}
