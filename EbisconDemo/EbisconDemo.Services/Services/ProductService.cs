using AutoMapper;
using EbisconDemo.Data.Interfaces;
using EbisconDemo.Data.Models;
using EbisconDemo.Services.Interfaces;
using EbisconDemo.Services.Models;

namespace EbisconDemo.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public IEnumerable<ProductDto> GetAllProducts()
        {
            var products = _productRepository.GetAll();

            var mapped = _mapper.Map<IEnumerable<ProductDto>>(products);

            return mapped;
        }

        public ProductDto GetProduct(int id)
        {
            var product = _productRepository.Get(id);

            if(product == null)
            {
                return null!;
            }

            var mapped = _mapper.Map<ProductDto>(product);

            return mapped;
        }
    }
}
