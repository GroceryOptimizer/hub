namespace Core.Entities
{
    internal class ShoppingCart
    {
        public int Id { get; set; }
        private List<StockItem> _cart { get; }

        public ShoppingCart(List<StockItem> cart)
        {
            _cart = cart;
        }
    }
}
