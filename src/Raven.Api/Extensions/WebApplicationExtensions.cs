using Scalar.AspNetCore;
using System.Diagnostics.CodeAnalysis;

namespace Raven.API.Extensions;

internal static class WebApplicationExtensions
{
    [ExcludeFromCodeCoverage]
    internal static WebApplication ConfigureRequestPipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference(options =>
            {
                options
                .WithTitle("Raven - OpenAPI Documentation")
                .WithFavicon("https://img.icons8.com/external-icongeek26-glyph-icongeek26/64/external-raven-birds-icongeek26-glyph-icongeek26.png");
            });

            app.MapGet("/", () => Results.Redirect("/scalar/v1"))
                .ExcludeFromDescription();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}