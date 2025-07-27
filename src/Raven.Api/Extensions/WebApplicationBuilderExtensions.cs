using Raven.API.Configurations;
using System.Diagnostics.CodeAnalysis;

namespace Raven.API.Extensions;

[ExcludeFromCodeCoverage]
internal static class WebApplicationBuilderExtensions
{
    internal static WebApplication ConfigureApplicationBuilder(this WebApplicationBuilder builder)
    {
        builder.Services.AddFactories();
        builder.Services.AddRepositories();
        builder.Services.ConfigureOptions();
        builder.Services.AddDomainServices();
        builder.Services.AddAbstractValidators();
        builder.Services.AddOpenApiDocumentation();
        builder.Services.AddControllersAndRouting();
        builder.Services.AddLogger(builder.Configuration);

        return builder.Build();
    }
}