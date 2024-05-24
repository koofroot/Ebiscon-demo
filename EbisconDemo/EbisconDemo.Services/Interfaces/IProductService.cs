using EbisconDemo.Services.Models;

namespace EbisconDemo.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();

        Task<ProductDto> GetProductAsync(int id);
    }
}
