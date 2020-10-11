using Coravel;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaDashboard.Application.Interfaces;
using SocialMediaDashboard.Infrastructure.Services;
using SocialMediaDashboard.Infrastructure.Tasks;
using VkNet;

namespace SocialMediaDashboard.Infrastructure.Extensions
{
    /// <summary>
    /// Service collection for Logic project.
    /// </summary>
    public static class InfrastructureExtension
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
            services.AddScoped<IObservationService, ObservationService>();
            services.AddScoped<IPlatformService, PlatformService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<ISubscriptionTypeService, SubscriptionTypeService>();
            services.AddScoped<IStatisticService, StatisticService>();
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<IVkService, VkService>();
            services.AddScoped<IInstagramService, InstagramService>();
            services.AddScoped<IYouTubeService, YouTubeService>();

            return services;
        }
    }
}
