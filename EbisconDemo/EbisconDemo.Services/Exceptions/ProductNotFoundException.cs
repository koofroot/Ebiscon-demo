
namespace EbisconDemo.Services.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(string message)
            :base(message)
        {            
        }
    }
}
