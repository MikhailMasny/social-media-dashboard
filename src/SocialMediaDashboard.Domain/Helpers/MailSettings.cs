namespace SocialMediaDashboard.Domain.Helpers
{
    /// <summary>
    /// Email settings (from appsettings.json).
    /// </summary>
    public class MailSettings
    {
        /// <summary>
        /// Server.
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Port.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Ssl or Tls.
        /// </summary>
        public bool UseSsl { get; set; }

        /// <summary>
        /// Address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        public string Password { get; set; }
    }
}
