using Coravel;
using Microsoft.AspNetCore.Builder;
using SocialMediaDashboard.Logic.Tasks;
using System;

namespace SocialMediaDashboard.Logic.Extensions
{
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
