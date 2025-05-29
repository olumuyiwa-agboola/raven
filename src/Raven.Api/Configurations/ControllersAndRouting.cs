using Raven.Core.Attributes;

namespace Raven.Api.Configurations
{
    internal static class ControllersAndRouting
    {
        internal static IServiceCollection AddControllersAndRouting(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<ValidateModelAttribute>();
            });
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

            return services;
        }
    }
}