using Raven.Core.Models.Shared;
using Raven.Core.Libraries.Constants;

namespace Raven.Core.Models.DTOs;

/// <summary>
/// Represents a data transfer object (DTO) for updating user information.
/// </summary>
/// <remarks>
/// This class is used to encapsulate the fields of a user's profile information that can
/// be updated. It includes properties for the user's last name, first name, phone number, 
/// and email address. At least one of these fields must be provided for an update operation.
/// </remarks>
public class UserUpdateDto
{
    public UserUpdateDto(string? firstName = null, string? lastName = null, string? phoneNumber = null, string? emailAddress = null)
    {
        if (string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName) &&
            string.IsNullOrWhiteSpace(phoneNumber) && string.IsNullOrWhiteSpace(emailAddress))
        {
            throw new ArgumentException("At least one field must be provided for update.");
        }

        LastName = new($"{DataStores.Users.Attributes.LastName}", lastName);
        FirstName = new($"{DataStores.Users.Attributes.FirstName}", firstName);
        PhoneNumber = new($"{DataStores.Users.Attributes.PhoneNumber}", phoneNumber);
        EmailAddress = new($"{DataStores.Users.Attributes.EmailAddress}", emailAddress);
    }

    /// <summary>
    /// An instance of the <see cref="Attribute"/> class containing the user's new last name and the associated database attribute name.
    /// </summary>
    public Shared.Attribute LastName { get; set; }

    /// <summary>
    /// An instance of the <see cref="Attribute"/> class containing the user's new first name and the associated database attribute name.
    /// </summary>
    public Shared.Attribute FirstName { get; set; }

    /// <summary>
    /// An instance of the <see cref="Attribute"/> class containing the user's new phone number and the associated database attribute name.
    /// </summary>
    public Shared.Attribute PhoneNumber { get; set; }

    /// <summary>
    /// An instance of the <see cref="Attribute"/> class containing the user's new email address and the associated database attribute name.
    /// </summary>
    public Shared.Attribute EmailAddress { get; set; }
}
