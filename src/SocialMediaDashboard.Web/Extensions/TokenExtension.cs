using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace SocialMediaDashboard.Web.Extensions
{
    /// <summary>
    /// Extensions for Identity Token.
    /// </summary>
    public static class TokenExtension
    {
        /// <summary>
        /// Encode token.
        /// </summary>
        /// <param name="token">Token.</param>
        /// <returns>Encoded token.</returns>
        public static string EncodeToken(this string token)
        {
            var tokenEncodedBytes = Encoding.UTF8.GetBytes(token);
            return WebEncoders.Base64UrlEncode(tokenEncodedBytes);
        }

        /// <summary>
        /// Decode token.
        /// </summary>
        /// <param name="token">Encoded token.</param>
        /// <returns>Decoded token.</returns>
        public static string DecodeToken(this string token)
        {
            var tokenDecodedBytes = WebEncoders.Base64UrlDecode(token);
            return Encoding.UTF8.GetString(tokenDecodedBytes);
        }
    }
}
