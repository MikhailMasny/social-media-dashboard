namespace SocialMediaDashboard.Web.Contracts.Requests
{
    /// <summary>
    /// Mail request.
    /// </summary>
    public class MailSettingsRequest
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
        /// User Ssl.
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
