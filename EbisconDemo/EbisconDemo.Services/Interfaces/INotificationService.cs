using EbisconDemo.Data.Interfaces;
using EbisconDemo.Services.Models;

namespace EbisconDemo.Services.Interfaces
{
    public interface INotificationService
    {
        NotificationsResponse GetNotifications(int userId);

        void NotifyOrderCreated(int orderId, string message, int? userId = null);

        void SetRead(int id);
    }
}
