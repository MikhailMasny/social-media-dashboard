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
        }
    }
}
