using Raven.Core.Enums;

namespace Raven.Core.Models.Shared
{
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

        public ErrorType Type { get; }

        public string Message { get; }

        public static Error NewError(ErrorType type, string message) => new(type, message);
    }
}
