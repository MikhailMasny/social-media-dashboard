using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SocialMediaDashboard.Application.Context;
using SocialMediaDashboard.Domain.Resources;
using System;

namespace SocialMediaDashboard.Web.Utils
{
    /// <summary>
    /// Fill in the database after creation.
    /// </summary>
    public static class ContextSeed
    {
        /// <summary>
        /// Context seed.
        /// </summary>
        /// <param name="serviceProvider">Service provider.</param>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            try
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                SocialMediaDashboardContextSeed.SeedRolesAsync(roleManager).GetAwaiter().GetResult();

                Log.Information(CommonResource.ContextSeedSuccessful);
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonResource.ContextSeedError);
            }
        }
    }
}
