using AutoMapper;
using EbisconDemo.Data.Interfaces;
using EbisconDemo.Data.Models;
using EbisconDemo.Services.Interfaces;
using EbisconDemo.Services.Models;
using EbisconDemo.Services.Models.Configuration;

namespace EbisconDemo.Services.Services
{
    public class SynchronizationService : ISynchronizationService
    {
        private readonly ProductSources _productSources;

        private readonly IDataRetrieveService _dataRetrieveService;
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public SynchronizationService(
            IDataRetrieveService dataRetrieveService,
            ProductSources productSources,
            IRepository<Product> productRepository,
            IMapper mapper)
        {
            _dataRetrieveService = dataRetrieveService;
            _productSources = productSources;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task SyncProductsAsync()
        {
            foreach (var source in _productSources.SynchronizationQueue)
            {
                var apiProducts = await _dataRetrieveService.GetProductsForAsync(source);
                var ourProducts = _productRepository.Get(x => x.SourceName.ToLower() == source.ToLower()).ToArray();

                var missingProducts = apiProducts
                    .ToArray()
                    .Where(their => !ourProducts.Any(our => our.ExternalId == their.Id))
                    .ToList();
                                
                var mapped = _mapper.Map<IEnumerable<ProductDto>, IEnumerable<Product>>(missingProducts)
                    .Select(x =>
                    {
                        x.SourceName = source;
                        return x;
                    })
                    .ToList();

                // No update for now
                _productRepository.Create(mapped);
            }

            _productRepository.Save();
        }
    }
}
