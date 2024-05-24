namespace EbisconDemo.Data.Models
{
    public class Rating : BaseEntity
    {
        public double Rate { get; set; }

        public int Count { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
