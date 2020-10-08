using Microsoft.AspNetCore.Http;
using SocialMediaDashboard.Application.Exceptions;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Web.Middlewares
{
    /// <summary>
    /// Error handler middleware.
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="next">Request delegate.</param>
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        /// <summary>
        /// Invoke.
        /// </summary>
        /// <param name="context">Http context.</param>
        public async Task Invoke(HttpContext context)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));

            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = error switch
                {
                    AppException _ => StatusCodes.Status400BadRequest,
                    NotFoundException _ => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError,
                };

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
