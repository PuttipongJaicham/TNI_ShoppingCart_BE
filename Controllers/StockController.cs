using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Model;
using StockService = ShoppingCart.Service.StockService;

namespace ShoppingCart.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly StockService _stockService;

        public StockController(StockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet("/GetStocks")]
        public async Task<ActionResult<List<Stock>>> GetStocks()
        {
            return Ok(await _stockService.GetStocksAsync());
        }

        [HttpPost("/UpdateStock")]
        public async Task<ActionResult<bool>> UpdateStock([FromBody] List<UpdateStockRequest> requests)
        {
            if (requests is null || requests.Count == 0)
            {
                return BadRequest("ต้องส่งรายการ productId และ orderCount อย่างน้อย 1 รายการ");
            }

            if (requests.Any(request => request.productId <= 0 || request.orderCount <= 0))
            {
                return BadRequest("productId และ orderCount ต้องมากกว่า 0");
            }

            var updated = await _stockService.UpdateStocksAsync(requests);

            if (!updated)
            {
                return Conflict("ไม่พบสินค้า หรือ stock ไม่เพียงพอ");
            }

            return Ok(true);
        }
    }
}
