using EbisconDemo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EbisconDemo.Api.Controllers
{
    [ApiController]    
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Authorize(Roles = "Customer,Manager,Admin")]
        public IActionResult Products()
        {
            var products = _productService.GetAllProducts();

            return Ok(products);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Customer,Manager,Admin")]
        public IActionResult GetProduct(int id)
        {
            var product = _productService.GetProduct(id);

            if(product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }
}
