namespace SocialMediaDashboard.Web.Constants
{
    /// <summary>
    /// API route constants.
    /// </summary>
    public static class ApiRoutes
    {
        /// <summary>
        /// Root path.
        /// </summary>
        public const string Root = "api";

        /// <summary>
        /// Identity path.
        /// </summary>
        public static class Identity
        {
            /// <summary>
            /// Identity path.
            /// </summary>
            public const string Path = "identity";

            /// <summary>
            /// Login endpoint.
            /// </summary>
            public const string Login = Root + "/" + Path + "/login";

            /// <summary>
            /// Registration endpoint.
            /// </summary>
            public const string Registration = Root + "/" + Path + "/registration";

            /// <summary>
            /// Confirm endpoint.
            /// </summary>
            public const string Confirm = Root + "/" + Path + "/confirm";

            /// <summary>
            /// Restore endpoint.
            /// </summary>
            public const string Restore = Root + "/" + Path + "/restore";

            /// <summary>
            /// Reset endpoint.
            /// </summary>
            public const string Reset = Root + "/" + Path + "/reset";

            /// <summary>
            /// Refresh token endpoint.
            /// </summary>
            public const string Refresh = Root + "/" + Path + "/refresh";
        }

        /// <summary>
        /// Config path.
        /// </summary>
        public static class Config
        {
            /// <summary>
            /// Config path.
            /// </summary>
            public const string Path = "config";

            /// <summary>
            /// Connection endpoint.
            /// </summary>
            public const string Connection = Root + "/" + Path + "/connection";

            /// <summary>
            /// JWT settings endpoint.
            /// </summary>
            public const string Token = Root + "/" + Path + "/token";

            /// <summary>
            /// Sentry settings endpoint.
            /// </summary>
            public const string Sentry = Root + "/" + Path + "/sentry";

            /// <summary>
            /// Social networks settings endpoint.
            /// </summary>
            public const string SocialNetworks = Root + "/" + Path + "/social-networks";
        }

        /// <summary>
        /// Subscription path.
        /// </summary>
        public static class Subscription
        {
            /// <summary>
            /// Config path.
            /// </summary>
            public const string Path = "subscription";

            /// <summary>
            /// Create endpoint.
            /// </summary>
            public const string Create = Root + "/" + Path;

            /// <summary>
            /// Get endpoint.
            /// </summary>
            public const string Get = Root + "/" + Path + "/{id}";

            /// <summary>
            /// Get all endpoint.
            /// </summary>
            public const string GetAll = Root + "/" + Path + "/all";

            /// <summary>
            /// Update endpoint.
            /// </summary>
            public const string Update = Root + "/" + Path + "/update/{id}";

            /// <summary>
            /// Delete endpoint.
            /// </summary>
            public const string Delete = Root + "/" + Path + "/delete/{id}";
        }
    }
}
