using Coravel;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaDashboard.Common.Interfaces;
using SocialMediaDashboard.Logic.Services;
using SocialMediaDashboard.Logic.Tasks;
using VkNet;

namespace SocialMediaDashboard.Logic.Extensions
{
    /// <summary>
    /// Service collection for Logic project.
    /// </summary>
    public static class LogicServiceCollectionExtension
    {
        /// <summary>
        /// Dependency injection.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <returns>Service collection.</returns>
        public static IServiceCollection AddLogic(this IServiceCollection services)
        {
            services.AddScheduler();
            services.AddTransient<StatisticInvocable>();

            services.AddSingleton(new VkApi());
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IConfigService, ConfigService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<IStatisticService, StatisticService>();
            services.AddScoped<IVkService, VkService>();
            services.AddScoped<IInstagramService, InstagramService>();

            return services;
        }
    }
}
