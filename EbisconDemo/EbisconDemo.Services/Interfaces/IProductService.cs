using EbisconDemo.Services.Models;

namespace EbisconDemo.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetAllProducts();

        ProductDto GetProduct(int id);
    }
}
