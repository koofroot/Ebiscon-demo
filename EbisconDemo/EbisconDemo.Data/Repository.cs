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

        public virtual void Create(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentException("Set cannot be null.", nameof(entities));
            }

            _dbSet.AddRange(entities);
        }

        public virtual void Create(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual T? Get(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual IEnumerable<T> Get(Func<T, bool> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
