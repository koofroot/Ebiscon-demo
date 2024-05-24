using EbisconDemo.Data.Models;

namespace EbisconDemo.Services.Models
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public string Message { get; set; }

        public virtual OrderDto Order { get; set; }
    }
}
