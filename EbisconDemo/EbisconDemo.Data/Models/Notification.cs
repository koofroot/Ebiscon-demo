namespace EbisconDemo.Data.Models
{
    public class Notification : BaseEntity
    {
        public bool IsRead { get; set; }

        public string Message { get; set; }

        public int? NotifyUserId { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public virtual User? NotifyUser { get; set; }
    }
}
