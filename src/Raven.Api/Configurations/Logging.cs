using Serilog;
using Raven.Core.Models.Configuration;

namespace Raven.Api.Configurations
{
    internal static class Logging
    {
        internal static IServiceCollection AddLogger(this IServiceCollection services, IConfiguration configuration)
        {
            var loggerSettings = configuration.GetSection(nameof(LoggerSettings)).Get<LoggerSettings>();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(loggerSettings!.FilePath!, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddSerilog();

            return services;
        }
    }
}
