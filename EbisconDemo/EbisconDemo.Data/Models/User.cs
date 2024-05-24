using EbisconDemo.Data.Models.Enums;

namespace EbisconDemo.Data.Models
{
    public class User : BaseEntity
    {

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual UserType UserType { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
