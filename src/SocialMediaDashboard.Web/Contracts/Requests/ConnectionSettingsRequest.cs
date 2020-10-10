namespace SocialMediaDashboard.Web.Contracts.Requests
{
    /// <summary>
    /// Connection strings settings request.
    /// </summary>
    public class ConnectionSettingsRequest
    {
        /// <summary>
        /// Microsoft SQL Server.
        /// </summary>
        public string MsSqlServerConnection { get; set; }

        /// <summary>
        /// PostgreSQL.
        /// </summary>
        public string PostgreSqlConnection { get; set; }
    }
}
