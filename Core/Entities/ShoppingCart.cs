namespace Core.Entities
{
    internal class ShoppingCart
    {
        public int Id { get; set; }
        public List<StockItem> Cart { get; set; }

        public ShoppingCart(List<StockItem> cart)
        {
            Cart = cart;
        }
    }
}
