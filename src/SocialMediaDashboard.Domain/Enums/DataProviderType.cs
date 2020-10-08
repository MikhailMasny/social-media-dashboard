namespace SocialMediaDashboard.Domain.Enums
{
    /// <summary>
    /// Application data providers.
    /// </summary>
    public enum DataProviderType
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// MS SQL Server.
        /// </summary>
        MSSQL = 1,

        /// <summary>
        /// Docker.
        /// </summary>
        Docker = 2,

        /// <summary>
        /// SQLite.
        /// </summary>
        SQLite = 3,

        /// <summary>
        /// PostgreSQL.
        /// </summary>
        PostgreSQL = 4
    }
}
