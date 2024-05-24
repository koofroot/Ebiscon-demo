using EbisconDemo.Data.Interfaces;
using EbisconDemo.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EbisconDemo.Data
{
    public class ProductRepository : Repository<Product>, IRepository<Product>
    {
        public ProductRepository(Context context) : base(context)
        {
        }

        public override IEnumerable<Product> GetAll()
        {
            return _dbSet.Include(x => x.Rating).ToList();
        }

        public override Product? Get(int id)
        {
            return _dbSet.Include(x => x.Rating).FirstOrDefault(x => x.Id == id);
        }
    }
}
