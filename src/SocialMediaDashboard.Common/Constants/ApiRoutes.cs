namespace SocialMediaDashboard.Common.Constants
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
            /// Login path.
            /// </summary>
            public const string Login = Root + "/identity/login";

            /// <summary>
            /// Registration path.
            /// </summary>
            public const string Registration = Root + "/identity/registration";

            /// <summary>
            /// Confirm path.
            /// </summary>
            public const string Confirm = Root + "/identity/confirm";
        }
    }
}
