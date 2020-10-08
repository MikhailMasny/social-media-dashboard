using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SocialMediaDashboard.Application.Context;
using System;

namespace SocialMediaDashboard.Web.Utils
{
    /// <summary>
    /// Apply migration in real time.
    /// </summary>
    public static class RuntimeMigration
    {
        private const string logErrorMessage = "An error occurred migrating the DB.";
        private const string logInformationMessage = "The database is successfully migrated.";

        /// <summary>
        /// Apply migration.
        /// </summary>
        /// <param name="serviceProvider">Service provider.</param>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            try
            {
                var appContextService = serviceProvider.GetRequiredService<SocialMediaDashboardContext>();
                appContextService.Database.Migrate();

                Log.Information(logInformationMessage);
            }
            catch (Exception ex)
            {
                Log.Error(ex, logErrorMessage);
            }
        }
    }
}
