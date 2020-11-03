using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Application.Interfaces
{
    /// <summary>
    /// Generic repository provide all base needed methods (CRUD)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Get all queries without tracking.
        /// </summary>
        /// <returns>IQueryable queries.</returns>
        IQueryable<T> GetAllWithoutTracking();

        /// <summary>
        /// Get all queries.
        /// </summary>
        /// <returns>IQueryable queries.</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Get entity async by predicate.
        /// </summary>
        /// <param name="predicate">LINQ predicate.</param>
        /// <returns>T entity.</returns>
        Task<T> GetEntityAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get entity async by predicate and without tracking.
        /// </summary>
        /// <param name="predicate">LINQ predicate.</param>
        /// <returns>T entity.</returns>
        Task<T> GetEntityWithoutTrackingAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Create new entity async.
        /// </summary>
        /// <param name="entity">Entity object</param>
        Task CreateAsync(T entity);

        /// <summary>
        /// Create new entities async.
        /// </summary>
        /// <param name="entities">Entity collection.</param>
        Task CreateRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity object</param>
        void Update(T entity);

        /// <summary>
        /// Remove entity from database.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        void Delete(T entity);

        /// <summary>
        /// Remove entities from database
        /// </summary>
        /// <param name="entity">Entity object</param>
        void DeleteRange(IEnumerable<T> entity);

        /// <summary>
        /// Persists all updates to the data source async.
        /// </summary>
        Task SaveChangesAsync();
    }
}
