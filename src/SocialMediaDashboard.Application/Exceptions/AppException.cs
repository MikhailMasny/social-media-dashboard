using System;
using System.Globalization;

namespace SocialMediaDashboard.Application.Exceptions
{
    /// <summary>
    /// Application exception.
    /// </summary>
    public class AppException : Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public AppException()
            : base()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Message.</param>
        public AppException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructor,
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="args">Arguments.</param>
        public AppException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public AppException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
