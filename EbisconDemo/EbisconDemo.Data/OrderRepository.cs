using EbisconDemo.Data.Interfaces;
using EbisconDemo.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EbisconDemo.Data
{
    public class OrderRepository : Repository<Order>, IRepository<Order>
    {
        public OrderRepository(Context context) : base(context)
        {
        }

        public override async Task<IEnumerable<Order>> GetAllAsync()
        {
            return _dbSet
                .Include(x => x.User)
                .Include(x => x.Product)
                .ToList();
        }

        public override async Task<Order?> GetAsync(int id)
        {
            return _dbSet
                .Include(x => x.User)
                .Include(x => x.Product)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
