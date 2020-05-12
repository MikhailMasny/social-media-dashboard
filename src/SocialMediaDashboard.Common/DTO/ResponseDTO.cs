namespace SocialMediaDashboard.Common.DTO
{
    /// <summary>
    /// Auth data transfer object.
    /// </summary>
    public class ResponseDTO
    {
        /// <summary>
        /// Operation result.
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// Message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// User data.
        /// </summary>
        public UserDTO User { get; set; }

        /// <summary>
        /// JWT Token.
        /// </summary>
        public string Token { get; set; }
    }
}
