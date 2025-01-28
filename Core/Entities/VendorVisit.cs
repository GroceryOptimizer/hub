namespace Core.Entities
{
    internal class VendorVisit
    {
        public int Id { get; set; }
        private Vendor _vendor { get; }
        private Coordinates _vendorCoordinates { get; }
        private ShoppingCart _shoppingCartAtThisVendor { get; }
        private int _totalPrice { get; }

        public VendorVisit(Vendor vendor, Coordinates coordinates, ShoppingCart shoppingCart, int totalPrice)
        {
            _vendor = vendor;
            _vendorCoordinates = coordinates;
            _shoppingCartAtThisVendor = shoppingCart;
            _totalPrice = totalPrice;
        }

    }
}
