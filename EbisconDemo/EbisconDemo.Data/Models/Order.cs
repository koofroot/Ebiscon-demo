using EbisconDemo.Data.Models.Enums;

namespace EbisconDemo.Data.Models
{
    public class Order : BaseEntity
    {
        public int Count { get; set; }

        public OrderStatus Status { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        public User User { get; set; }

        public Product Product { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
