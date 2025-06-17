using FluentValidation;
using MySql.Data.MySqlClient;
using Raven.Core.Abstractions.Factories;
using Raven.Core.Libraries.Enums;
using Raven.Core.Models.Configuration;

namespace Raven.Core.Libraries.Constants
{
    /// <summary>
    /// Provides a set of predefined validation rules to use on properties of request models.
    /// </summary>
    /// <remarks>The <see cref="ValidationRules"/> class contains static methods that define common validation
    /// rules for properties, such as ensuring a property is required or not empty. These rules can be  applied
    /// to objects during validation using a fluent API.</remarks>
    public static class ValidationRules
    {
        /// <summary>
        /// Specifies that the string property is required and must not be empty.
        /// </summary>
        /// <remarks>This method adds a validation rule to ensure that the string property is not empty.
        /// If the property is empty, a validation error will be generated with the message "Is required".</remarks>
        /// <typeparam name="T">The type of the object being validated.</typeparam>
        /// <param name="ruleBuilder">The rule builder used to define validation rules for the string property.</param>
        /// <returns>An <see cref="IRuleBuilderOptions{T, string}"/> instance that allows further configuration of the validation
        /// rule.</returns>
        public static IRuleBuilderOptions<T, string> IsRequired<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Is not provided.");
        }

        public static IRuleBuilderOptions<T, string> MustBeAValidEmailAddress<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .EmailAddress()
                .WithMessage("Is not a valid email address.");
        }

        public static IRuleBuilderOptions<T, SearchParameter> MustBeAValidSearchType<T>(this IRuleBuilder<T, SearchParameter> ruleBuilder)
        {
            return ruleBuilder
                .IsInEnum().WithMessage("Must be 'UserId' or 'EmailAddress' or 'PhoneNumber'");
        }

        public static IRuleBuilderOptions<T, string> MustBeAValidFilePath<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return (IRuleBuilderOptions<T, string>)ruleBuilder.Custom((filePath, context) =>
            {
                if (!File.Exists(filePath))
                {
                    context.AddFailure("File path does not exist.");
                }
            });
        }

        public static IRuleBuilderOptions<T, string?> MustBeAbleToEstablishAMySqlDatabaseConnection<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return (IRuleBuilderOptions<T, string?>)ruleBuilder.Custom((value, context) =>
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    context.AddFailure("Connection string is required.");
                }
                else
                {
                    try
                    {
                        using var connection = new MySqlConnection(value);
                        connection.Open();
                    }
                    catch (Exception ex)
                    {
                        context.AddFailure($"Connection string is invalid: {ex.Message}");
                    }
                }
            });
        }

        public static IRuleBuilderOptions<T, string> MustNotExceed<T>(this IRuleBuilder<T, string> ruleBuilder, int maximumLength)
        {
            return ruleBuilder
                .MaximumLength(maximumLength)
                .WithMessage($"Exceeds {maximumLength} characters.");
        }

        public static IRuleBuilderOptions<T, string> MustContainOnlyAllowedCharactersForNames<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Matches("^[A-Za-z0-9-_ ]+$")
                .WithMessage($"Contains unallowed characters.");
        }

        public static IRuleBuilderOptions<T, string> MustContainOnlyAllowedCharactersForUserIds<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Matches("^[A-Za-z0-9]+$")
                .WithMessage($"Contains unallowed characters.");
        }

        public static IRuleBuilderOptions<T, string> MustContainOnlyAllowedCharactersForEmailAddresses<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Matches("^[A-Za-z0-9-_ .@]+$")
                .WithMessage($"Contains unallowed characters.");
        }

        public static IRuleBuilderOptions<T, string> MustContainOnlyAllowedCharactersForPhoneNumbers<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Matches("^[+0-9]+$")
                .WithMessage($"Contains unallowed characters.");
        }
    }
}
