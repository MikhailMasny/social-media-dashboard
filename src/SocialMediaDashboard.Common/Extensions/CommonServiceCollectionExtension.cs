using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SocialMediaDashboard.Common.Extensions
{
    /// <summary>
    /// Service collection for Common project.
    /// </summary>
    public static class CommonServiceCollectionExtension
    {
        /// <summary>
        /// Dependency injection.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <returns>Service collection.</returns>
        public static IServiceCollection AddCommon(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
