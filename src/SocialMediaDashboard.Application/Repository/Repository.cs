using Microsoft.EntityFrameworkCore;
using SocialMediaDashboard.Application.Context;
using SocialMediaDashboard.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Application.Repository
{
    /// <inheritdoc cref="IRepository<T>"</>
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        private readonly DbContext _context;

        public Repository(ApplicationContext context)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));

            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <inheritdoc/>
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        /// <inheritdoc/>
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        /// <inheritdoc/>
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        /// <inheritdoc/>
        public void DeleteRange(IEnumerable<T> entity)
        {
            _dbSet.RemoveRange(entity);
        }

        /// <inheritdoc/>
        public IQueryable<T> GetAllWithoutTracking()
        {
            return _dbSet.AsNoTracking();
        }

        /// <inheritdoc/>
        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        /// <inheritdoc/>
        public async Task<T> GetEntityAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        /// <inheritdoc/>
        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
