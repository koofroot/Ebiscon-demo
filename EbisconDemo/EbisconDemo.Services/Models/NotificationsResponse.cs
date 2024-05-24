namespace EbisconDemo.Services.Models
{
    public class NotificationsResponse
    {
        public DateTime IssuedAt { get; set; }

        public int IssuedFor { get; set; }

        public IEnumerable<NotificationDto> Notifications { get; set; }
    }
}
