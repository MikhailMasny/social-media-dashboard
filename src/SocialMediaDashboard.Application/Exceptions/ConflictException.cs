using System;

namespace SocialMediaDashboard.Application.Exceptions
{
    /// <summary>
    /// Conflict exception.
    /// </summary>
    public class ConflictException : Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ConflictException()
            : base()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Message.</param>
        public ConflictException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public ConflictException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="key">Key.</param>
        public ConflictException(string name, object key)
            : base($"An error occurred while interacting with the entity \"{name}\" ({key}).")
        {
        }
    }
}
