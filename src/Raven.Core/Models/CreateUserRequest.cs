using FluentValidation;

namespace Raven.Core.Models
{
    /// <summary>
    /// Represents the body of a POST request to create a new user with the specified details.
    /// </summary>
    /// <remarks>This class is used to encapsulate the data required to create a new user obtained 
    /// from the body of a POST request to the relevant endpoint.</remarks>
    public record CreateUserRequest
    {
        /// <summary>
        /// Gets or initializes the email address associated with the user.
        /// </summary>
        public required string Email { get; init; }

        /// <summary>
        /// Gets or initializes the last name of the user.
        /// </summary>
        public required string LastName { get; init; }

        /// <summary>
        /// Gets or initializes the first name of the user.
        /// </summary>
        public required string FirstName { get; init; }

        /// <summary>
        /// Gets or initializes the phone number of the user.
        /// </summary>
        public required string PhoneNumber { get; init; }
    }

    /// <summary>
    /// Provides validation rules for the <see cref="CreateUserRequest"/> model.
    /// </summary>
    /// <remarks>This validator ensures that the required fields in a <see cref="CreateUserRequest"/> instance
    /// are populated and meet specific format requirements. The following rules are applied: <list type="bullet">
    /// <item><description><c>Email</c> must not be empty and must be a valid email address.</description></item>
    /// <item><description><c>LastName</c> must not be empty.</description></item> <item><description><c>FirstName</c>
    /// must not be empty.</description></item> <item><description><c>PhoneNumber</c> must not be
    /// empty.</description></item> </list></remarks>
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Invalid email address.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last name is required.");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First name is required.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required.");
        }
    }
}
