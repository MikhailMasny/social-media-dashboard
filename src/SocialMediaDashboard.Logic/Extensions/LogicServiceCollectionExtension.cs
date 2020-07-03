﻿using Microsoft.Extensions.DependencyInjection;
using SocialMediaDashboard.Common.Interfaces;
using SocialMediaDashboard.Logic.Services;
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
            services.AddSingleton(new VkApi());
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IConfigService, ConfigService>();
            services.AddScoped<IVkService, VkService>();

            return services;
        }
    }
}
