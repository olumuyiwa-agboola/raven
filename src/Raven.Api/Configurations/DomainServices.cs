using Raven.Core.Services;
using System.Diagnostics.CodeAnalysis;
using Raven.Core.Abstractions.Services;

namespace Raven.API.Configurations;

internal static class DomainServices
{
    [ExcludeFromCodeCoverage]
    internal static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IUsersService, UsersService>();

        return services;
    }
}
