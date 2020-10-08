using Microsoft.AspNetCore.Builder;
using SocialMediaDashboard.Web.Middlewares;
using System;

namespace SocialMediaDashboard.Web.Extensions
{
    /// <summary>
    /// Error handler extension.
    /// </summary>
    public static class ErrorHandlerExtension
    {
        /// <summary>
        /// Custom error handler.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <returns>Application builder with custom error handler.</returns>
        public static IApplicationBuilder UseCustomErrorHandler(this IApplicationBuilder app)
        {
            app = app ?? throw new ArgumentNullException(nameof(app));

            app.UseMiddleware<ErrorHandlerMiddleware>();

            return app;
        }
    }
}
