using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Gmail_POC.API.Models
{
    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState)
            : base(new ValidationResultModel(modelState))
        {
                
        }

        public ValidationFailedResult(ModelStateDictionary modelState, int? statusCode )
           : base(new ValidationResultModel(modelState))
        {
            StatusCode = statusCode;

        }
    }

}
