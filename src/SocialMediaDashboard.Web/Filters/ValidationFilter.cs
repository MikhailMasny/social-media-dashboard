using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SocialMediaDashboard.Web.Contracts.Responses;
using SocialMediaDashboard.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Web.Filters
{
    /// <summary>
    /// Validation filter.
    /// </summary>
    public class ValidationFilter : IAsyncActionFilter
    {
        /// <summary>
        /// Execute filter.
        /// </summary>
        /// <param name="context">Application context.</param>
        /// <param name="next">Next delegate.</param>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));
            next = next ?? throw new ArgumentNullException(nameof(next));

            if (!context.ModelState.IsValid)
            {
                var errorsInModelState = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

                var errorResponse = new ErrorResponse();

                foreach (var error in errorsInModelState)
                {
                    foreach (var subError in error.Value)
                    {
                        var errorModel = new ValidationErrorModel
                        {
                            Field = error.Key,
                            Message = subError
                        };

                        errorResponse.Errors.Add(errorModel);
                    }
                }

                context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }

            await next();
        }
    }
}
