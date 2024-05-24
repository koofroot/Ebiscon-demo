namespace EbisconDemo.Data.Models
{
    public class Product : BaseEntity
    {
        public Product()
        {
            Orders = new HashSet<Order>();
        }

        public int ExternalId { get; set; }
        public string SourceName { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }

        public int RatingId { get; set; }

        public Rating Rating { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
