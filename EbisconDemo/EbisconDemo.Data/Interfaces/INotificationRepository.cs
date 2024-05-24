using EbisconDemo.Data.Models;

namespace EbisconDemo.Data.Interfaces
{
    public interface INotificationRepository : IRepository<Notification>
    {
        IEnumerable<Notification> GetUserNotifications(int userId);

        void SetRead(int id);
    }
}
