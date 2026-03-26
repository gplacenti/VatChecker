using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ValidationFilter<T>(IValidator<T> validator) : IAsyncActionFilter where T : class
{
    private readonly IValidator<T> _validator = validator;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var param = context.ActionArguments.Values.OfType<T>().FirstOrDefault();

        if (param == null)
        {
            context.Result = new BadRequestObjectResult("Request body is required.");
            return;
        }

        var validationResult = await _validator.ValidateAsync(param);

        if (!validationResult.IsValid)
        {
            context.Result = new BadRequestObjectResult(new
            {
                Errors = validationResult.Errors.Select(e => e.ErrorMessage)
            });
            return;
        }

        await next();
    }
}