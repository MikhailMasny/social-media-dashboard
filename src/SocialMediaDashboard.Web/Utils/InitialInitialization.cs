using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace SocialMediaDashboard.Web.Utils
{
    /// <summary>
    /// Run initial services in a specific scope.
    /// </summary>
    public class InitialInitialization
    {
        /// <summary>
        /// Build a factory for initital tasks.
        /// </summary>
        /// <param name="host">Application host.</param>
        public static void Run(IHost host)
        {
            host = host ?? throw new ArgumentNullException(nameof(host));

            using IServiceScope scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            RuntimeMigration.Initialize(services);
            ContextSeed.Initialize(services);
        }
    }
}
