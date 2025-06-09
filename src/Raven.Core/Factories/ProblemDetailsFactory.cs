using System.Net;
using Microsoft.AspNetCore.Mvc;
using Raven.Core.Models.Shared;
using Raven.Core.Libraries.Enums;
using Raven.Core.Libraries.Constants;

namespace Raven.Core.Factories
{
    public class ProblemDetailsFactory
    {
        public static ProblemDetails CreateProblemDetailsFromError(Error error)
        {
            return error.Type switch
            {
                ErrorType.UserNotFound => new ProblemDetails()
                {
                    Instance = null,
                    Detail = error.Message,
                    Title = ErrorMessages.UserNotFound,
                    Status = (int)HttpStatusCode.UnprocessableEntity,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.9"
                },
                ErrorType.RecordAlreadyExists => new ProblemDetails()
                {
                    Instance = null,
                    Detail = error.Message,
                    Status = (int)HttpStatusCode.Conflict,
                    Title = ErrorMessages.RecordAlreadyExists,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.9"
                },
                _ => new ProblemDetails()
                {
                    Instance = null,
                    Detail = error.Message,
                    Title = ErrorMessages.InternalServerError,
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
                Title = ErrorMessages.FailedValidations,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            };

            validationFailures.ToList().ForEach(failure =>
                validationProblemDetails.Errors.Add(failure.Key, [failure.Value])
            );

            return validationProblemDetails;
        }
    }
}
