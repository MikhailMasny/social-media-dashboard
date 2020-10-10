using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SocialMediaDashboard.Domain.Constants;
using SocialMediaDashboard.Domain.Helpers;
using SocialMediaDashboard.Web.Filters;
using System;
using System.Text;

namespace SocialMediaDashboard.Web.Extensions
{
    /// <summary>
    /// Service collection for Web project.
    /// </summary>
    public static class WebServiceCollectionExtension
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
                RequireExpirationTime = false,
                ValidateLifetime = true
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

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API",
                    Description = "Social Media Dashboard API",
                    Contact = new OpenApiContact()
                    {
                        Name = "Mikhail M. & Alexandr G.",
                        //Url = new Uri("https://social-media-dashboard-api.herokuapp.com/") // UNDONE: add it after deploy
                    }
                });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                config.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        securitySchema, new[] { "Bearer" }
                    }
                };
                config.AddSecurityRequirement(securityRequirement);
            });

            services.AddSwaggerGenNewtonsoftSupport();

            services.AddCors();
            services.AddHealthChecks();
            services.AddControllers(options =>
                options.Filters.Add<ValidationFilter>())
                    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.ConfigureWritable<ConnectionSettings>(configuration.GetSection(ConnectionString.AppSettings));
            services.ConfigureWritable<SentrySettings>(configuration.GetSection(nameof(SentrySettings)));
            services.ConfigureWritable<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
            services.ConfigureWritable<SocialNetworksSettings>(configuration.GetSection(nameof(SocialNetworksSettings)));

            return services;
        }
    }
}
