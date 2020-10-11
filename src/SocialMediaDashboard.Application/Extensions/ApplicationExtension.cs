using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialMediaDashboard.Application.Context;
using SocialMediaDashboard.Application.Interfaces;
using SocialMediaDashboard.Application.Repository;
using SocialMediaDashboard.Domain.Constants;
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
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SocialMediaDashboardContext>(options => options.UseSqlServer(configuration.GetConnectionString(ConnectionString.MsSqlServer)));

            // TODO: for production
            //services.AddDbContext<SocialMediaDashboardContext>(options =>
            //{
            //    options.UseNpgsql(configuration.GetConnectionString(ConnectionString.PostgreSql));
            //    options.UseSecondLevelCache();
            //});

            services.AddDefaultIdentity<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<SocialMediaDashboardContext>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}
