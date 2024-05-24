using EbisconDemo.Data.Interfaces;
using EbisconDemo.Services.Models;

namespace EbisconDemo.Services.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationsResponse> GetNotificationsAsync(int userId);

        Task NotifyOrderCreatedAsync(int orderId, string message, int? userId = null);

        Task SetReadAsync(int id);
    }
}
