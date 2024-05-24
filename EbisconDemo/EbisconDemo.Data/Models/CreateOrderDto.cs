namespace EbisconDemo.Data.Models
{
    public class CreateOrderDto
    {
        public int ProductId { get; set; }

        public int UserId { get; set; }

        public int Count { get; set; }
    }
}
