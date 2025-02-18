namespace Core.Entities
{
    public class StockItem
    {
        public Product Product { get; set; } = null;
        public int Quantity { get; set; }
        public int Price { get; set; }

        public StockItem(Product product, int quantity, int price)
        {
            Product = product;
            Quantity = quantity;
            Price = price;
        }
    }
}
