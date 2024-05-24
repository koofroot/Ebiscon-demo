using EbisconDemo.Data.Models;

namespace EbisconDemo.Data.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetAsync(int id);

        Task<IEnumerable<T>> GetAsync(Func<T, bool> predicate);

        Task<IEnumerable<T>> GetAllAsync();

        Task CreateAsync(IEnumerable<T> entities);

        Task CreateAsync(T entity);

        Task SaveAsync();
    }
}
