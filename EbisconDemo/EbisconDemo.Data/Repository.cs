using EbisconDemo.Data.Interfaces;
using EbisconDemo.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EbisconDemo.Data
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbSet<T> _dbSet;
        protected readonly Context _context;

        public Repository(Context context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual Task CreateAsync(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentException("Set cannot be null.", nameof(entities));
            }

            return _dbSet.AddRangeAsync(entities);
        }

        public virtual Task CreateAsync(T entity)
        {
            return _dbSet.AddAsync(entity).AsTask();
        }

        public virtual async Task<T?> GetAsync(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual async Task<IEnumerable<T>> GetAsync(Func<T, bool> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return _dbSet.ToList();
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
