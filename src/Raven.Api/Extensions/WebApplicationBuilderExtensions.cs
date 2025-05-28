using Raven.Api.Configurations;

namespace Raven.Api.Extensions
{
    internal static class WebApplicationBuilderExtensions
    {
        internal static WebApplication ConfigureApplicationBuilder(this WebApplicationBuilder builder)
        {
            builder.Services.ConfigureOptions(builder.Configuration);
            builder.Services.AddControllersAndRouting();
            builder.Services.AddOpenApiDocumentation();
            builder.Services.AddDomainServices();
            builder.Services.AddRepositories();
            builder.Services.AddFactories();

            return builder.Build();
        }
    }
}