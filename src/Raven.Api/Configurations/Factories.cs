using Raven.Infrastructure.Factories;
using Raven.Core.Abstractions.Factories;

namespace Raven.API.Configurations
{
    internal static class Factories
    {
        internal static IServiceCollection AddFactories(this IServiceCollection services)
        {
            services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();

            return services;
        }
    }
}
