using FluentValidation;
using Raven.Core.Libraries.Constants;

namespace Raven.Core.Models.Requests
{
    /// <summary>
    /// Represents the body of a POST request to update a user's information.
    /// </summary>
    /// <remarks>This class is used to encapsulate the data required to update a user's profile,
    /// obtained from the body of a POST request to the relevant endpoint. All properties are 
    /// optional, and any property left unset will not be updated.</remarks>
    public record UpdateUserRequest
    {
        /// <summary>
        /// The new last name of the user.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// The new first name of the user.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// The new phone number of the user.
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// The new email address of the user.
        /// </summary>
        public string EmailAddress { get; set; } = string.Empty;
    }

    /// <summary>
    /// Provides validation rules for the <see cref="UpdateUserRequest"/> model.
    /// </summary>
    /// <remarks>This validator ensures that the required fields in a <see cref="UpdateUserRequest"/> instance
    /// are populated and meet specific format requirements. The following rules are applied: <list type="bullet">
    /// <item><description><c>Email</c> must not be empty and must be a valid email address.</description></item>
    /// <item><description><c>LastName</c> must not be empty.</description></item> <item><description><c>FirstName</c>
    /// must not be empty.</description></item> <item><description><c>PhoneNumber</c> must not be
    /// empty.</description></item> </list></remarks>
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.EmailAddress)
                .IsRequired()
                .MustNotExceed(200)
                .MustBeAValidEmailAddress()
                .MustContainOnlyAllowedCharactersForEmailAddresses();

            RuleFor(x => x.LastName)
                .IsRequired()
                .MustNotExceed(100)
                .MustContainOnlyAllowedCharactersForNames();

            RuleFor(x => x.FirstName)
                .IsRequired()
                .MustNotExceed(100)
                .MustContainOnlyAllowedCharactersForNames();

            RuleFor(x => x.PhoneNumber)
                .IsRequired()
                .MustNotExceed(20)
                .MustContainOnlyAllowedCharactersForPhoneNumbers();
        }
    }
}
