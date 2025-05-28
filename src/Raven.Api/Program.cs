using Raven.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

var app = builder.ConfigureApplicationBuilder();

app.ConfigureRequestPipeline()
    .Run();