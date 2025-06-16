using FluentValidation;

namespace Raven.Core.Models.Configuration
{
    /// <summary>
    /// Represents the configuration settings for a logger.
    /// </summary>
    /// <remarks>This class provides properties to configure the behavior of a logger, such as the file path
    /// where log entries should be written. Use this class to specify logging preferences when initializing or
    /// configuring a logging system.</remarks>
    public class LoggerSettings
    {
        public string? FilePath { get; set; }
    }

    /// <summary>
    /// Provides validation rules for <see cref="LoggerSettings"/> to ensure its properties meet required conditions.
    /// </summary>
    /// <remarks>This validator enforces that the:
    /// <list type="bullet">
    ///     <item><description><c>FilePath</c> property is not empty.</description></item>
    /// </list> 
    /// 
    /// Use this class to validate instances of <see cref="LoggerSettings"/> before using them in logging
    /// operations.</remarks>
    public class LoggerSettingsValidator : AbstractValidator<LoggerSettings>
    {
        public LoggerSettingsValidator()
        {
            RuleFor(model => model.FilePath)
                .NotEmpty().WithMessage($"{nameof(LoggerSettings.FilePath)} is required");
        }
    }
}
