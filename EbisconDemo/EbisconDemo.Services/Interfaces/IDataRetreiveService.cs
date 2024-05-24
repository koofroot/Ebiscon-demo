using EbisconDemo.Services.Models;

namespace EbisconDemo.Services.Interfaces
{
    public interface IDataRetrieveService
    {
        Task<IEnumerable<ProductDto>> GetProductsForAsync(string sourceName);
    }
}
