using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SocialMediaDashboard.Application.Context;
using SocialMediaDashboard.Domain.Resources;
using System;

namespace SocialMediaDashboard.Web.Utils
{
    /// <summary>
    /// Apply migration in real time.
    /// </summary>
    public static class RuntimeMigration
    {
        /// <summary>
        /// Apply migration.
        /// </summary>
        /// <param name="serviceProvider">Service provider.</param>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            try
            {
                var hostEnironmentService = serviceProvider.GetRequiredService<IHostEnvironment>();
                if (hostEnironmentService.IsProduction())
                {
                    var appContextService = serviceProvider.GetRequiredService<SocialMediaDashboardContext>();
                    appContextService.Database.Migrate();
                }

                Log.Information(CommonResource.DatabaseMigrateSuccessful);
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonResource.DatabaseMigrateError);
            }
        }
    }
}
