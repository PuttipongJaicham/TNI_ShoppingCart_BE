using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Model;

namespace ShoppingCart.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdductsController : ControllerBase
    {

        [HttpGet]
        public List<Product> GetProducts()
        {
            var products = new List<Product>();
            return products;
        }

        [HttpGet]
        public Product GetProductsById(int id)
        {
            var product = new Product();
            return product;
        }
    }
}
