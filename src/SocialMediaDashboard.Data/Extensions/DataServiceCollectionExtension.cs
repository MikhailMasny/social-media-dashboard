using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(configuration.GetConnectionString("MSSQLConnection")));

            // UNDONE: Change it to IdentityServer4
            services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<ApplicationContext>()
                    .AddDefaultTokenProviders();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}
