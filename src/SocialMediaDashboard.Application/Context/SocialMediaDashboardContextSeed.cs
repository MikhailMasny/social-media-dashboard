using Microsoft.AspNetCore.Identity;
using SocialMediaDashboard.Domain.Constants;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Application.Context
{
    /// <summary>
    /// SocialMediaDashboard context seed.
    /// </summary>
    public static class SocialMediaDashboardContextSeed
    {
        /// <summary>
        /// Seed roles.
        /// </summary>
        /// <param name="context">SocialMediaDashboard context.</param>
        /// <param name="roleManager">Role manager.</param>
        public static async Task SeedRolesAsync(SocialMediaDashboardContext context, RoleManager<IdentityRole> roleManager)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));
            roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));

            if (context.Roles.Any())
            {
                return;
            }

            if (!await roleManager.RoleExistsAsync(AppRole.Admin))
            {
                var adminRole = new IdentityRole(AppRole.Admin);
                await roleManager.CreateAsync(adminRole);
            }

            if (!await roleManager.RoleExistsAsync(AppRole.User))
            {
                var userRole = new IdentityRole(AppRole.User);
                await roleManager.CreateAsync(userRole);
            }
        }
    }
}
