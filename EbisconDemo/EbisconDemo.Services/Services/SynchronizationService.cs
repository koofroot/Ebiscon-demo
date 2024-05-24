using AutoMapper;
using EbisconDemo.Data.Interfaces;
using EbisconDemo.Data.Models;
using EbisconDemo.Services.Interfaces;
using EbisconDemo.Services.Models;
using EbisconDemo.Services.Models.Configuration;
using System.Collections.Concurrent;
using System.Linq;

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
            var theirProductsBag = new ConcurrentBag<ProductDto>();
            var tasks = _productSources.SynchronizationQueue
                .AsParallel()
                .WithDegreeOfParallelism(5)
                .Select(async sourceName => 
                { 
                    (await _dataRetrieveService.GetProductsForAsync(sourceName))
                    .ToList()
                    .ForEach(product => 
                    {
                        product.SourceName = sourceName;
                        theirProductsBag.Add(product);
                    }); 
                })
                .ToArray();

            var ourProductsTask = _productRepository
                .GetAsync(x => _productSources.SynchronizationQueue.Select(x => x.ToLower()).Contains(x.SourceName.ToLower()));

            await Task.WhenAll([.. tasks, ourProductsTask]);

            var missingProducts = theirProductsBag
                    .ToArray()
                    .Where(their => !ourProductsTask.Result.Any(our => our.ExternalId == their.Id && our.SourceName == their.SourceName))
                    .ToList();

            var mapped = _mapper.Map<IEnumerable<ProductDto>, IEnumerable<Product>>(missingProducts);

            await _productRepository.CreateAsync(mapped);

            await _productRepository.SaveAsync();
        }
    }
}
