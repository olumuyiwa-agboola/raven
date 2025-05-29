using System.Net;
using Microsoft.AspNetCore.Mvc;
using Raven.Core.Models.Shared;
using Raven.Core.Libraries.Enums;

namespace Raven.Core.Factories
{
    public class ProblemDetailsFactory
    {
        public static ProblemDetails CreateProblemDetailsFromError(Error error)
        {
            return error.Type switch
            {
                ErrorType.RecordAlreadyExists => new ProblemDetails()
                {
                    Instance = null,
                    Detail = error.Message,
                    Title = "Record already exists.",
                    Status = (int)HttpStatusCode.Conflict,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.9"
                },
                _ => new ProblemDetails()
                {
                    Instance = null,
                    Detail = error.Message,
                    Title = "Internal server error.",
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                },
            };
        }

        public static ValidationProblemDetails CreateValidationProblemDetails(Dictionary<string, string> validationFailures, string instance)
        {
            var validationProblemDetails = new ValidationProblemDetails()
            {
                Instance = instance,
                Status = (int)HttpStatusCode.BadRequest,
                Title = "One or more validation errors occurred.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            };

            validationFailures.ToList().ForEach(failure =>
                validationProblemDetails.Errors.Add(failure.Key, [failure.Value])
            );

            return validationProblemDetails;
        }
    }
}
