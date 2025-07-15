using Raven.Core.Abstractions.Repositories;
using Raven.Infrastructure.Repositories.MySQL;

namespace Raven.API.Configurations
{
    internal static class Repositories
    {
        internal static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUsersRepository, UsersMySQLRepository>();

            return services;
        }
    }
}
