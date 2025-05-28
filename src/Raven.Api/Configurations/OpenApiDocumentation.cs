namespace Raven.Api.Configurations
{
    internal static class OpenApiDocumentation
    {
        internal static IServiceCollection AddOpenApiDocumentation(this IServiceCollection services)
        {
            services.AddOpenApi();

            return services;
        }
    }
}