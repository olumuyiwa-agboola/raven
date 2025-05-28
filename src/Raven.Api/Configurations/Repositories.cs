using Raven.Core.Abstractions.Repositories;
using Raven.Infrastructure.Repositories.MySQL;

namespace Raven.Api.Configurations
{
    internal static class Repositories
    {
        internal static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOtpsRepository, OtpsMySQLRepository>();
            services.AddScoped<IUsersRepository, UsersMySQLRepository>();

            return services;
        }
    }
}
