using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;
using System.Diagnostics.CodeAnalysis;
using Olumuyiwa.DotNetDevKit.AspNetCore.ActionFilters;

namespace Raven.API.Configurations;

internal static class ControllersAndRouting
{
    [ExcludeFromCodeCoverage]
    internal static IServiceCollection AddControllersAndRouting(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ValidateRequestParametersAttribute>();
        });
        services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });

        return services;
    }
}