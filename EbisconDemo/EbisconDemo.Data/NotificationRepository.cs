using EbisconDemo.Data.Interfaces;
using EbisconDemo.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EbisconDemo.Data
{
    public class NotificationRepository : Repository<Notification>, IRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(Context context) : base(context)
        {
        }

        public async Task<IEnumerable<Notification>> GetUserNotificationsAsync(int userId)
        {
            return _dbSet
                .Where(x => (x.NotifyUserId == null || x.NotifyUserId == userId) && !x.IsRead)
                .Include(x => x.Order)
                .Include(x => x.Order.Product)
                .Include(x => x.Order.User)
                .ToList();
        }

        public async Task SetReadAsync(int id)
        {
            var notification = await _dbSet.FindAsync(id);

            if(notification == null)
            {
                throw new ArgumentException("No notification for given ID.");
            }

            notification.IsRead = true;
        }
    }
}
