namespace Core.Entities
{
    public class ShoppingCart
    {
        public List<Product> Cart { get; set; } = new List<Product>();

        public ShoppingCart(List<Product> cart)
        {
            Cart = cart;
        }
    }
}
