using Raven.Core.Attributes;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Raven.API.Configurations
{
    internal static class ControllersAndRouting
    {
        [ExcludeFromCodeCoverage]
        internal static IServiceCollection AddControllersAndRouting(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<ValidateModelAttribute>();
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
}