﻿namespace SocialMediaDashboard.Web.Contracts.Requests
{
    /// <summary>
    /// ConnectionStrings request.
    /// </summary>
    public class ConnectionSettingsRequest
    {
        /// <summary>
        /// MS SQL Server.
        /// </summary>
        public string MSSQLConnection { get; set; }

        /// <summary>
        /// Docker.
        /// </summary>
        public string DockerConnection { get; set; }

        /// <summary>
        /// SQLite.
        /// </summary>
        public string SQLiteConnection { get; set; }

        /// <summary>
        /// PostgreSQL.
        /// </summary>
        public string PostgreSQLConnection { get; set; }
    }
}