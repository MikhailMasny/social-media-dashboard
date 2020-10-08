using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace SocialMediaDashboard.Web.Extensions
{
    /// <summary>
    /// Extensions for JWT Token.
    /// </summary>
    public static class JwtTokenExtensions
    {
        /// <summary>
        /// Get user identifier.
        /// </summary>
        /// <param name="httpContext">Application HttpContext.</param>
        /// <returns>Identifier.</returns>
        public static string GetUserId(this HttpContext httpContext)
        {
            httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));

            if (httpContext.User == null)
            {
                return string.Empty;
            }

            return httpContext.User.Claims.SingleOrDefault(claim => claim.Type == "id").Value;
        }

        /// <summary>
        /// Get user role.
        /// </summary>
        /// <param name="httpContext">Application HttpContext.</param>
        /// <returns>Role.</returns>
        public static string GetUserRole(this HttpContext httpContext)
        {
            httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));

            if (httpContext.User == null)
            {
                return string.Empty;
            }

            return httpContext.User.Claims.SingleOrDefault(claim => claim.Type.Contains("role", StringComparison.InvariantCulture)).Value;
        }
    }
}
