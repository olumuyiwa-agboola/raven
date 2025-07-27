using Raven.Core.Libraries.Enums;

namespace Raven.Core.Models.Shared;

/// <summary>
/// Represents an error that occured during the execution of a method.
/// </summary>
/// <remarks>This class is used to hold information about an error that occured during the execution of a
/// method. It holds the <see cref="ErrorType"/> and the error message <see langword="string"/>.</remarks>
/// <param name="type">The error type.</param>
/// <param name="message">The error message.</param>
public class Error
{
    private Error(ErrorType type, string message)
    {
        Type = type;
        Message = message;
    }

    /// <summary>
    /// The error type.
    /// </summary>
    public ErrorType Type { get; }

    /// <summary>
    /// The error message.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="Error"/> class with the specified error type and message.
    /// </summary>
    /// <param name="type">The error type.</param>
    /// <param name="message">The error message.</param>
    /// <returns>An instance of the <see cref="Error"/> class.</returns>
    public static Error NewError(ErrorType type, string message) => new(type, message);
}
