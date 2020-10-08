using AutoMapper;
using EntityFrameworkCore.Cacheable;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialMediaDashboard.Application.Context;
using SocialMediaDashboard.Application.Interfaces;
using SocialMediaDashboard.Application.Repository;
using SocialMediaDashboard.Domain.Entities;
using System.Reflection;

namespace SocialMediaDashboard.Application.Extensions
{
    /// <summary>
    /// Service collection for Data project.
    /// </summary>
    public static class ApplicationExtension
    {
        /// <summary>
        /// Dependency injection.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="configuration">Configuration.</param>
        /// <param name="environment">Host environment.</param>
        /// <returns>Service collection.</returns>
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            //if (environment.IsDevelopment())
            //{
            //    services.AddDbContext<SocialMediaDashboardContext>(options => options.UseSqlServer(configuration.GetConnectionString("MSSQLConnection")));
            //}
            //else
            //{
            //    services.AddDbContext<SocialMediaDashboardContext>(options =>
            //    {
            //        options.UseNpgsql(configuration.GetConnectionString("PostgreSQLConnection"));
            //        options.UseSecondLevelCache();
            //    });
            //}

            services.AddDbContext<SocialMediaDashboardContext>(options => options.UseSqlServer(configuration.GetConnectionString("MSSQLConnection")));

            // UNDONE: Change it to IdentityServer4
            services.AddDefaultIdentity<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<SocialMediaDashboardContext>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}
