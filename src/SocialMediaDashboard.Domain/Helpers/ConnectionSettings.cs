namespace SocialMediaDashboard.Domain.Helpers
{
    /// <summary>
    /// ConnectionStrings settings (from appsettings.json).
    /// </summary>
    public class ConnectionSettings
    {
        /// <summary>
        /// MS SQL Server.
        /// </summary>
        public string MsSqlServerConnection { get; set; }

        /// <summary>
        /// PostgreSQL.
        /// </summary>
        public string PostgreSqlConnection { get; set; }
    }
}
