﻿using Microsoft.EntityFrameworkCore;
using SocialMediaDashboard.Domain.Models;

namespace SocialMediaDashboard.Data.Context
{
    /// <summary>
    /// Database context.
    /// </summary>
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options">DbContextOptions.</param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            // UNDONE: fix it to Migrate
            Database.EnsureCreated();
        }

        /// <summary>
        /// User entities.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Media entities.
        /// </summary>
        public DbSet<Media> Medias { get; set; }

        /// <summary>
        /// Statistic entities.
        /// </summary>
        public DbSet<Statistic> Statistics { get; set; }
    }
}