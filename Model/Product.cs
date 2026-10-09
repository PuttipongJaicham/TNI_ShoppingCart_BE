namespace ShoppingCart.Model
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public int price { get; set; }
        public int stockTotal { get; set; }
        public int? orderCount { get; set; } = 0;
    }
}
