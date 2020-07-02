using EntityFrameworkCore.Cacheable;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialMediaDashboard.Common.Interfaces;
using SocialMediaDashboard.Data.Context;
using SocialMediaDashboard.Data.Repository;

namespace SocialMediaDashboard.Data.Extensions
{
    /// <summary>
    /// Service collection for Data project.
    /// </summary>
    public static class DataServiceCollectionExtension
    {
        /// <summary>
        /// Dependency injection.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="configuration">Configuration.</param>
        /// <returns>Service collection.</returns>
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(configuration.GetConnectionString("MSSQLConnection")));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(options => {
                    options.UseNpgsql(configuration.GetConnectionString("PostgreSQLConnection"));
                    options.UseSecondLevelCache();
                });
            }

            // UNDONE: Change it to IdentityServer4
            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}
