using FluentValidation;
using Raven.Core.Models.Requests;
using System.Diagnostics.CodeAnalysis;

namespace Raven.API.Configurations
{
    internal static class AbstractValidators
    {
        [ExcludeFromCodeCoverage]
        internal static IServiceCollection AddAbstractValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateUserRequestValidator>();

            return services;
        }
    }
}