namespace Core.Entities
{
    public class StockItem
    {
        public Product Product { get; set; } = null;
        public int Price { get; set; }

        public StockItem(Product product, int price)
        {
            Product = product;
            Price = price;
        }
    }
}
