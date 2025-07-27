using FluentValidation;
using Microsoft.Extensions.Options;
using Raven.Core.Models.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Raven.API.Configurations;

internal static class Options
{
    [ExcludeFromCodeCoverage]
    internal static IServiceCollection ConfigureOptions(this IServiceCollection services)
    {
        services.AddOptionsWithFluentValidation<LoggerSettings, LoggerSettingsValidator>(nameof(LoggerSettings));
        services.AddOptionsWithFluentValidation<ConnectionStrings, ConnectionStringsValidator>(nameof(ConnectionStrings));

        return services;
    }

    [ExcludeFromCodeCoverage]
    private static IServiceCollection AddOptionsWithFluentValidation<TOptions,
            TOptionsValidator>(this IServiceCollection services, string configurationSection)
            where TOptions : class, new() where TOptionsValidator : AbstractValidator<TOptions>
    {
        services.AddScoped<IValidator<TOptions>, TOptionsValidator>();

        services.AddOptions<TOptions>()
            .BindConfiguration(configurationSection)
            .ValidateOptionsWithFluentValidation()
            .ValidateOnStart();

        return services;
    }

    [ExcludeFromCodeCoverage]
    private static OptionsBuilder<TOptions> ValidateOptionsWithFluentValidation<TOptions>(this OptionsBuilder<TOptions> builder) where TOptions : class
    {
        builder.Services.AddSingleton<IValidateOptions<TOptions>>(
            serviceProvider => new OptionsFluentValidationHandler<TOptions>(
                serviceProvider,
                builder.Name));

        return builder;
    }
}

file class OptionsFluentValidationHandler<TOptions>(IServiceProvider serviceProvider, string? name) : IValidateOptions<TOptions> where TOptions : class
{
    private readonly string? _name = name;

    [ExcludeFromCodeCoverage]
    public ValidateOptionsResult Validate(string? name, TOptions options)
    {
        if (_name != null && _name != name)
        {
            return ValidateOptionsResult.Skip;
        }

        ArgumentNullException.ThrowIfNull(options, nameof(options));

        using var scope = serviceProvider.CreateScope();

        var validator = scope.ServiceProvider.GetRequiredService<IValidator<TOptions>>();

        var result = validator.Validate(options);

        if (result.IsValid)
        {
            return ValidateOptionsResult.Success;
        }

        var type = options.GetType().Name;
        var errors = new List<string>();

        foreach (var error in result.Errors)
        {
            errors.Add($"Validation failed for {type}.{error.PropertyName}: with error: {error.ErrorMessage}");
        }

        return ValidateOptionsResult.Fail(errors);
    }
}
