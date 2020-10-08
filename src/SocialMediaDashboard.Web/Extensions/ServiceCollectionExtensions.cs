using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SocialMediaDashboard.Application.Interfaces;
using SocialMediaDashboard.Infrastructure.Options;

namespace SocialMediaDashboard.Web.Extensions
{
    /// <summary>
    /// Extensions for ServiceCollection.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Extension method to overwrite configurations.
        /// </summary>
        /// <typeparam name="T">Generic class.</typeparam>
        /// <param name="services">Service collection.</param>
        /// <param name="section">Configuration section.</param>
        /// <param name="file">Application settings file.</param>
        public static void ConfigureWritable<T>(this IServiceCollection services, IConfigurationSection section, string file = "appsettings.json") where T : class, new()
        {
            services.Configure<T>(section);
            services.AddTransient<IWritableOptions<T>>(provider =>
            {
                var environment = provider.GetService<IHostEnvironment>();
                var options = provider.GetService<IOptionsMonitor<T>>();
                return new WritableOptions<T>(environment, options, section.Key, file);
            });
        }
    }
}
