namespace Core.Entities
{
    public class StockItem
    {
        public int Id { get; set; } // Primary Key
        public int ProductId { get; set; }  // Foreign Key
        public Product Product { get; set; } // Navigation Property
        public int Quantity { get; set; }
        public int Price { get; set; }

        /*
        public StockItem(Product product, int quantity, int price)
        {
            Product = product;
            Quantity = quantity;
            Price = price;
        }
        */
    }
}
