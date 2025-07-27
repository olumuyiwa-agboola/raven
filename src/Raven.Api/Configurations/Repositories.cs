using System.Diagnostics.CodeAnalysis;
using Raven.Core.Abstractions.Repositories;
using Raven.Infrastructure.Repositories.MySQL;

namespace Raven.API.Configurations;

internal static class Repositories
{
    [ExcludeFromCodeCoverage]
    internal static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UsersMySQLRepository>();

        return services;
    }
}
