using Raven.Core.Services;
using Raven.Core.Abstractions.Services;

namespace Raven.Api.Configurations
{
    internal static class DomainServices
    {
        internal static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IOtpsService, OtpsService>();
            services.AddScoped<IUsersService, UsersService>();

            return services;
        }
    }
}
