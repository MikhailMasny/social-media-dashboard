using Microsoft.Extensions.Options;
using System;

namespace SocialMediaDashboard.Application.Interfaces
{
    /// <summary>
    /// Interface for rewriting configurations (appsettings.json).
    /// </summary>
    /// <typeparam name="T">Generic class.</typeparam>
    public interface IWritableOptions<out T> : IOptionsSnapshot<T> where T : class, new()
    {
        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="applyChanges">Apply changes.</param>
        void Update(Action<T> applyChanges);
    }
}
