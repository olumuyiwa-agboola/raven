namespace Raven.Core.Models.DTOs
{
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

            LastName = new("last_name", lastName);
            FirstName = new("first_name", firstName);
            PhoneNumber = new("phone_number", phoneNumber);
            EmailAddress = new("email_address", emailAddress);
        }

        public Property LastName { get; set; }

        public Property FirstName { get; set; }

        public Property PhoneNumber { get; set; }

        public Property EmailAddress { get; set; }
    }

    public record Property(string ColumnName, string? Value)
    {
        public string? Value = Value;

        public string ColumnName = ColumnName;
    }}
