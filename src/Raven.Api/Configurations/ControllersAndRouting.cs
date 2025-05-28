namespace Raven.Api.Configurations
{
    internal static class ControllersAndRouting
    {
        internal static IServiceCollection AddControllersAndRouting(this IServiceCollection services)
        {
            services.AddControllers();

            return services;
        }
    }
}