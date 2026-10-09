using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Model;
using ProductService = ShoppingCart.Service.ProductService;

namespace ShoppingCart.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("/GetProducts")]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return Ok(await _productService.GetProductsAsync());
        }

        [HttpGet("/GetProductsById/{id:int}")]
        public async Task<ActionResult<Product>> GetProductsById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }
}
