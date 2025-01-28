namespace Core.Entities
{
    internal class VendorVisit
    {
        public int Id { get; set; }
        public Vendor Vendor { get; set; }
        public Coordinates VendorCoordinates { get; set; }
        public ShoppingCart ShoppingCartAtThisVendor { get; set; }
        public int TotalPrice { get; set; }

        public VendorVisit(Vendor vendor, Coordinates coordinates, ShoppingCart shoppingCart, int totalPrice)
        {
            Vendor = vendor;
            VendorCoordinates = coordinates;
            ShoppingCartAtThisVendor = shoppingCart;
            TotalPrice = totalPrice;
        }

    }
}
