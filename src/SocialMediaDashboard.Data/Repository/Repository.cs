using Microsoft.EntityFrameworkCore;
using SocialMediaDashboard.Common.Interfaces;
using SocialMediaDashboard.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Data.Repository
{
    /// <inheritdoc cref="IRepository<T>"</>
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        private readonly DbContext _context;
        
        public Repository(ApplicationContext context)
        {
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
        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        /// <inheritdoc/>
        public async Task<T> GetEntity(Expression<Func<T, bool>> predicate)
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
