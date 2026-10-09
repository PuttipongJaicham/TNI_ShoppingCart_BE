namespace ShoppingCart.Model;

public class UpdateStockRequest
{
    public int productId { get; set; }
    public int orderCount { get; set; }
}
