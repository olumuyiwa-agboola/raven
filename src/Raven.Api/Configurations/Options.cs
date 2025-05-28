using Raven.Core.Configuration;

namespace Raven.Api.Configurations
{
    internal static class Options
    {
        internal static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));

            return services;
        }
    }
}
