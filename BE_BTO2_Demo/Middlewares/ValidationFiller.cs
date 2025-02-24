using BE_BTO2_Demo.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BE_BTO2_Demo.Middlewares
{
    public class ValidationFiller : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if(context == null) throw new ArgumentNullException(nameof(context));

            if (!context.ModelState.IsValid) 
            {
                var erros = context.ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToList()
                    );

                var response = ApiResponse<object>.Error("Validation failed");
                response.Data = erros;

                context.Result = new BadRequestObjectResult(response);
            }
        }
    }
}
