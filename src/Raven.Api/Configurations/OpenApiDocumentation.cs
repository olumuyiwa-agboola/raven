using System.Diagnostics.CodeAnalysis;

namespace Raven.API.Configurations
{
    internal static class OpenApiDocumentation
    {
        [ExcludeFromCodeCoverage]
        internal static IServiceCollection AddOpenApiDocumentation(this IServiceCollection services)
        {
            services.AddOpenApi(options =>
            {
                options.AddDocumentTransformer((document, context, cancellationToken) =>
                {
                    document.Info = new()
                    {
                        Title = "Raven",
                        Version = "v1.0",
                        Description = "RESTful API for user management.",
                        Contact = new()
                        {
                            Email = "agboolaod@gmail.com",
                            Name = "Olumuyiwa Agboola"
                        }
                    };
                    return Task.CompletedTask;
                });
            });

            return services;
        }
    }
}