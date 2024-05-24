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

        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            return _dbSet.Include(x => x.Rating).ToList();
        }

        public override async Task<Product?> GetAsync(int id)
        {
            return _dbSet.Include(x => x.Rating).FirstOrDefault(x => x.Id == id);
        }
    }
}
