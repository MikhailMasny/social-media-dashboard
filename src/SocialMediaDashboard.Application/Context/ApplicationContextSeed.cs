using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Application.Context
{
    /// <summary>
    /// Application context seed.
    /// </summary>
    public static class ApplicationContextSeed
    {
        /// <summary>
        /// Seed roles.
        /// </summary>
        /// <param name="applicationContext">Application context.</param>
        /// <param name="userManager">User manager.</param>
        /// <param name="roleManager">Role manager.</param>
        /// <returns></returns>
        public static async Task SeedRolesAsync(SocialMediaDashboardContext applicationContext, RoleManager<IdentityRole> roleManager)
        {
            applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
            roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));

            if (applicationContext.Roles.Any())
            {
                return;
            }

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                var adminRole = new IdentityRole("Admin");
                await roleManager.CreateAsync(adminRole);
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                var userRole = new IdentityRole("User");
                await roleManager.CreateAsync(userRole);
            }
        }
    }
}
