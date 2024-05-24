using EbisconDemo.Data.Models;

namespace EbisconDemo.Data.Interfaces
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<IEnumerable<Notification>> GetUserNotificationsAsync(int userId);

        Task SetReadAsync(int id);
    }
}
