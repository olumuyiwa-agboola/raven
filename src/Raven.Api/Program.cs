using Raven.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

var app = builder.ConfigureApplicationBuilder();

app.ConfigureRequestPipeline()
    .Run();

/// To make the implicit Program class public so that test projects can access it
public partial class Program { }