using Raven.Infrastructure.Factories;
using System.Diagnostics.CodeAnalysis;
using Raven.Core.Abstractions.Factories;

namespace Raven.API.Configurations
{
    internal static class Factories
    {
        [ExcludeFromCodeCoverage]
        internal static IServiceCollection AddFactories(this IServiceCollection services)
        {
            services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();

            return services;
        }
    }
}
