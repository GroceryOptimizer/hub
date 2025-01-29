namespace Core.Entities
{
    public class StockItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }  // Foreign key
        public Product Product { get; set; } = null;
        public int Amount { get; set; }

        public int ShoppingCartId { get; set; }  // Foreign key
        public ShoppingCart? ShoppingCart { get; set; }

        public StockItem() { } // Required for EF Core

        public StockItem(Product product, int amount)
        {
            Product = product;
            Amount = amount;
        }
    }
}
