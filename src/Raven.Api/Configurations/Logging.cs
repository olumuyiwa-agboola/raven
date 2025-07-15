using Serilog;
using Serilog.Events;
using Raven.Core.Models.Configuration;

namespace Raven.API.Configurations
{
    internal static class Logging
    {
        internal static IServiceCollection AddLogger(this IServiceCollection services, IConfiguration configuration)
        {
            var loggerSettings = configuration.GetSection(nameof(LoggerSettings)).Get<LoggerSettings>();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(
                    shared: true,
                    rollOnFileSizeLimit: true,
                    path: loggerSettings!.FilePath!,
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 10_000_000, // 10 MB
                    restrictedToMinimumLevel: LogEventLevel.Debug,
                    flushToDiskInterval: TimeSpan.FromSeconds(1))
                .CreateLogger();

            services.AddSerilog();

            return services;
        }
    }
}
