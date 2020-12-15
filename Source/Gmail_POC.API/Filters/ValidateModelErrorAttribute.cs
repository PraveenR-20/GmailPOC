using Gmail_POC.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Gmail_POC.API.Filters
{
    public class ValidateModelErrorAttribute : ActionFilterAttribute
    {
        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var result = (ObjectResult)context.Result;
                context.Result = new ValidationFailedResult(context.ModelState, result.StatusCode);
            }

            return base.OnResultExecutionAsync(context, next);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var result = (ObjectResult)context.Result;
                context.Result = new ValidationFailedResult(context.ModelState, result.StatusCode);             
            }

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.ModelState.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                var result = (ObjectResult) context.Result;
                context.Result = new ValidationFailedResult(context.ModelState, result.StatusCode);
            }

            base.OnActionExecuted(context);
        }
    }
}
