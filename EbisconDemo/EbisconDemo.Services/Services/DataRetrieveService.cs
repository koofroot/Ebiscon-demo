using EbisconDemo.Services.Interfaces;
using EbisconDemo.Services.Models;
using EbisconDemo.Services.Models.Configuration;
using System.Text.Json;

namespace EbisconDemo.Services.Services
{
    public class DataRetrieveService : IDataRetrieveService
    {
        private readonly ProductSources _productSources;

        private readonly HttpClient _httpClient;

        public DataRetrieveService(HttpClient httpClient, ProductSources productSources)
        {
            _httpClient = httpClient;

            _productSources = productSources;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsForAsync(string sourceName)
        {
            var sourceUri = _productSources.Sources.GetValueOrDefault(sourceName);
            if(sourceUri == null)
            {
                throw new ArgumentException("Source name does not exist.", nameof(sourceName));
            }

            var response = await _httpClient.GetAsync(sourceUri);
            
            var content = await response.Content.ReadAsStringAsync();

            var products = JsonSerializer.Deserialize<IEnumerable<ProductDto>>(content);

            return products;
        }
    }
}
