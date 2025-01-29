namespace Core.Entities
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public ICollection<StockItem> Cart { get; set; } = new List<StockItem>();

        public ShoppingCart() { } // Required for EF Core

        public ShoppingCart(List<StockItem> cart)
        {
            Cart = cart;
        }
    }
}
