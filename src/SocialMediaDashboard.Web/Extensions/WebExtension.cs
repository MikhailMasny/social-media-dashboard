using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SocialMediaDashboard.Application.Context;
using SocialMediaDashboard.Domain.Constants;
using SocialMediaDashboard.Domain.Entities;
using SocialMediaDashboard.Domain.Helpers;
using SocialMediaDashboard.Web.Constants;
using SocialMediaDashboard.Web.Filters;
using System;
using System.Text;

namespace SocialMediaDashboard.Web.Extensions
{
    /// <summary>
    /// Service collection for Web project.
    /// </summary>
    public static class WebExtension
    {
        /// <summary>
        /// Dependency injection.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="configuration">Configuration.</param>
        /// <returns>Service collection.</returns>
        public static IServiceCollection AddWeb(this IServiceCollection services, IConfiguration configuration)
        {
            configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            services.AddJwt(configuration);
            services.AddAppIdentity();
            services.AddSwagger();
            services.AddCoreBase();
            services.AddWritableOptions(configuration);

            return services;
        }

        private static IServiceCollection AddAppIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<SocialMediaDashboardContext>()
                .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(IdentityConstant.TokenLifetime));

            return services;
        }

        private static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettingsSection = configuration.GetSection(nameof(JwtSettings));
            services.Configure<JwtSettings>(jwtSettingsSection);

            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
            var jwtSecretKey = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            var tokenValidationParametrs = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(jwtSecretKey),
                ValidateIssuer = false,
                ValidateAudience = false,

                // Another possible settings
                //ValidIssuer = JwtParametrs.Issuer,
                //ValidAudience = JwtParametrs.Audience,
                //ClockSkew = TimeSpan.Zero,
                //RequireExpirationTime = false,
                //ValidateLifetime = true
            };
            services.AddSingleton(tokenValidationParametrs);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = tokenValidationParametrs;
            });

            return services;
        }

        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc(SwaggerParameters.Version, new OpenApiInfo
                {
                    Version = SwaggerParameters.Version,
                    Title = SwaggerParameters.Title,
                    Description = SwaggerParameters.Description,
                    Contact = new OpenApiContact()
                    {
                        Name = SwaggerParameters.Contact.Name,
                        //Url = new Uri("https://social-media-dashboard-api.herokuapp.com/") // UNDONE: add it after deploy
                    }
                });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = SwaggerParameters.Security.Description,
                    Name = SwaggerParameters.Security.Name,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = SwaggerParameters.Security.HttpAuth,
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = SwaggerParameters.Security.Schema
                    }
                };
                config.AddSecurityDefinition(SwaggerParameters.Security.Schema, securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        securitySchema, new[] { SwaggerParameters.Security.Schema }
                    }
                };
                config.AddSecurityRequirement(securityRequirement);
            });

            services.AddSwaggerGenNewtonsoftSupport();

            return services;
        }

        private static IServiceCollection AddCoreBase(this IServiceCollection services)
        {
            services.AddCors();
            services.AddHealthChecks();
            services.AddControllers(options =>
                options.Filters.Add<ValidationFilter>())
                    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>())
                .AddNewtonsoftJson();

            return services;
        }

        private static IServiceCollection AddWritableOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureWritable<ConnectionSettings>(configuration.GetSection(ConnectionString.AppSettings));
            services.ConfigureWritable<SentrySettings>(configuration.GetSection(nameof(SentrySettings)));
            services.ConfigureWritable<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
            services.ConfigureWritable<SocialNetworksSettings>(configuration.GetSection(nameof(SocialNetworksSettings)));
            services.ConfigureWritable<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));

            return services;
        }
    }
}
