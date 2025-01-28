namespace Core.Entities
{
    internal class StockItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }

        public StockItem(Product product, int amount)
        {
            Product = product;
            Amount = amount;
        }

    }
}
