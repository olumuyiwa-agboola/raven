using FluentValidation;

namespace Raven.Core.Models.Configuration
{
    public class ConnectionStrings
    {
        public string? RavenMySQLConnectionString { get; set; }
    }

    public class ConnectionStringsValidator : AbstractValidator<ConnectionStrings>
    {
        public ConnectionStringsValidator()
        {
            RuleFor(model => model.RavenMySQLConnectionString)
                .NotEmpty().WithMessage($"{nameof(ConnectionStrings.RavenMySQLConnectionString)} is required");
        }
    }
}
