using FluentValidation;
using Raven.Core.Models.Requests;

namespace Raven.Api.Configurations
{
    internal static class AbstractValidators
    {
        internal static IServiceCollection AddAbstractValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateOtpUserRequestValidator>();

            return services;
        }
    }
}