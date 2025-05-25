using Raven.Core.Services;
using Raven.Core.Configuration;
using Raven.Infrastructure.Factories;
using Raven.Core.Abstractions.Services;
using Raven.Core.Abstractions.Factories;
using Raven.Core.Abstractions.Repositories;
using Raven.Infrastructure.Repositories.MySQL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddScoped<IOtpsService, OtpsService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IOtpsRepository, OtpsRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();

builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();