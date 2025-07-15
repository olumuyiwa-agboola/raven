using Raven.Core.Services;
using Raven.Core.Abstractions.Services;

namespace Raven.API.Configurations
{
    internal static class DomainServices
    {
        internal static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IUsersService, UsersService>();

            return services;
        }
    }
}
