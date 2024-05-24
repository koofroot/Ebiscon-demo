using AutoMapper;
using EbisconDemo.Data.Interfaces;
using EbisconDemo.Data.Models;
using EbisconDemo.Services.Interfaces;
using EbisconDemo.Services.Models;

namespace EbisconDemo.Services.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        public NotificationService(
            INotificationRepository notificationRepository, 
            IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        public async Task<NotificationsResponse> GetNotificationsAsync(int userId)
        {
            if(userId <= 0)
            {
                throw new ArgumentException("User ID cannot be empty.", nameof(userId));
            }

            var notifications = await _notificationRepository.GetUserNotificationsAsync(userId);

            if (!notifications.Any())
            {
                return new NotificationsResponse
                {
                    IssuedAt = DateTime.UtcNow,
                    IssuedFor = userId
                };
            }

            var mapped = _mapper.Map<IEnumerable<NotificationDto>>(notifications);

            var dto = new NotificationsResponse
            {
                IssuedAt = DateTime.UtcNow,
                IssuedFor = userId,
                Notifications = mapped
            };

            return dto;
        }

        public async Task NotifyOrderCreatedAsync(int orderId, string message, int? userId = null)
        {
            if(orderId <= 0 || string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Order ID or Message cannot be empty.");
            }

            var notification = new Notification
            {
                NotifyUserId = userId,
                OrderId = orderId,
                Message = message
            };

            await _notificationRepository.CreateAsync(notification);

            await _notificationRepository.SaveAsync();
        }

        public async Task SetReadAsync(int id)
        {
            await _notificationRepository.SetReadAsync(id);

            await _notificationRepository.SaveAsync();
        }
    }
}
