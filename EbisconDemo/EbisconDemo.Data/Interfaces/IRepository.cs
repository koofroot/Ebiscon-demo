using EbisconDemo.Data.Models;

namespace EbisconDemo.Data.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T? Get(int id);

        IEnumerable<T> Get(Func<T, bool> predicate);

        IEnumerable<T> GetAll();

        void Create(IEnumerable<T> entities);

        void Create(T entity);

        void Save();
    }
}
