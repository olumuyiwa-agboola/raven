using FluentValidation;
using Raven.Core.Factories;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Raven.Core.Attributes
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var serviceProvider = context.HttpContext.RequestServices;

            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument == null)
                {
                    context.Result = new BadRequestObjectResult("Request body cannot be null.");
                    return;
                }

                var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());

                if (serviceProvider.GetService(validatorType) is IValidator validator)
                {
                    ValidationResult validationResult = validator.Validate(new ValidationContext<object>(argument));

                    if (!validationResult.IsValid)
                    {
                        Dictionary<string, string> validationFailures = [];
                        foreach (ValidationFailure failure in validationResult.Errors)
                        {
                            if (validationFailures.ContainsKey(failure.PropertyName))
                                validationFailures[failure.PropertyName] += " | " + failure.ErrorMessage;
                            else
                                validationFailures[failure.PropertyName] = failure.ErrorMessage;
                        }

                        var validationProblemDetails = ProblemDetailsFactory.CreateValidationProblemDetails(validationFailures, context.HttpContext.Request.Path);

                        context.Result = new BadRequestObjectResult(validationProblemDetails);

                        return;
                    }
                }
            }

            base.OnActionExecuting(context);
        }
    }
}