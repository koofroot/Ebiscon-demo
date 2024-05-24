namespace EbisconDemo.Services.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Status { get; set; }

        public int Count { get; set; }

        public UserDto User { get; set; }

        public ProductDto Product { get; set; }
    }
}
