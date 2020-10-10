using Coravel;
using Microsoft.AspNetCore.Builder;
using SocialMediaDashboard.Infrastructure.Tasks;
using System;

namespace SocialMediaDashboard.Infrastructure.Extensions
{
    // TODO: transfer to worker service

    /// <summary>
    /// Coravel extension.
    /// </summary>
    public static class CoravelExtension
    {
        /// <summary>
        /// Use coravel.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <returns>Application builder with coravel.</returns>
        public static IApplicationBuilder UseCoravel(this IApplicationBuilder app)
        {
            app = app ?? throw new ArgumentNullException(nameof(app));

            var provider = app.ApplicationServices;
            provider.UseScheduler(scheduler =>
            {
                scheduler.Schedule<StatisticInvocable>()
                    .EveryMinute();
            });

            return app;
        }
    }
}
