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

        public IEnumerable<Notification> GetUserNotifications(int userId)
        {
            return _dbSet
                .Where(x => (x.NotifyUserId == null || x.NotifyUserId == userId) && !x.IsRead)
                .Include(x => x.Order)
                .Include(x => x.Order.Product)
                .Include(x => x.Order.User)
                .ToList();
        }

        public void SetRead(int id)
        {
            var notification = _dbSet.Find(id);

            if(notification == null)
            {
                throw new ArgumentException("No notification for given ID.");
            }

            notification.IsRead = true;
        }
    }
}
