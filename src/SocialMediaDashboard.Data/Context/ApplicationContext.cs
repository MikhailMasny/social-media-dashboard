using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialMediaDashboard.Data.Configurations;
using SocialMediaDashboard.Domain.Entities;
using System;

namespace SocialMediaDashboard.Data.Context
{
    /// <summary>
    /// Database context.
    /// </summary>
    public class ApplicationContext : IdentityDbContext
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options">DbContextOptions.</param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) { }

        /// <summary>
        /// Media entities.
        /// </summary>
        public DbSet<Media> Medias { get; set; }

        /// <summary>
        /// Statistic entities.
        /// </summary>
        public DbSet<Statistic> Statistics { get; set; }

        /// <summary>
        /// Profile entities.
        /// </summary>
        public DbSet<Statistic> Profiles { get; set; }

        /// <summary>
        /// RefreshToken entities.
        /// </summary>
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.ApplyConfiguration(new ProfileConfiguration());
            modelBuilder.ApplyConfiguration(new MediaConfiguration());
            modelBuilder.ApplyConfiguration(new StatisticConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
