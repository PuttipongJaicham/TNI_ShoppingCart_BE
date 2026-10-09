using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Model;

namespace ShoppingCart.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {

        [HttpGet]
        public Stock CheckStockByProductId()
        {
            var stock = new Stock();
            return stock;
        }

        [HttpPost]
        public bool UpdateStock()
        {
            var result = false;
            return result;
        }
    }
}
