using FluentValidation;

namespace Raven.Core.Models.Configuration
{
    public class LoggerSettings
    {
        public string? FilePath { get; set; }
    }

    public class LoggerSettingsValidator : AbstractValidator<LoggerSettings>
    {
        public LoggerSettingsValidator()
        {
            RuleFor(model => model.FilePath)
                .NotEmpty().WithMessage($"{nameof(LoggerSettings.FilePath)} is required");
        }
    }
}
