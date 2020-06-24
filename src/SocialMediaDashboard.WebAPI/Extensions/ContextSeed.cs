using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SocialMediaDashboard.Data.Context;
using System;

namespace SocialMediaDashboard.WebAPI.Extensions
{
    /// <summary>
    /// Fill in the database after creation.
    /// </summary>
    public static class ContextSeed
    {
        private const string logErrorMessage = "An error occurred seeding the DB.";
        private const string logInformationMessage = "The database is successfully seeded.";

        /// <summary>
        /// Context seed.
        /// </summary>
        /// <param name="serviceProvider">Service provider.</param>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            try
            {
                var contextOptions = serviceProvider.GetRequiredService<DbContextOptions<ApplicationContext>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                using var applicationContext = new ApplicationContext(contextOptions);

                ApplicationContextSeed.SeedRolesAsync(applicationContext, roleManager).GetAwaiter().GetResult();

                Log.Information(logInformationMessage);
            }
            catch (Exception ex)
            {
                Log.Error(ex, logErrorMessage);
            }
        }
    }
}
