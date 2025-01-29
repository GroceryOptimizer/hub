namespace Core.Entities
{
    public class VendorVisit
    {
        public int Id { get; set; }
        public int VendorId { get; set; }  // Foreign key
        public Vendor Vendor { get; set; } = null;
        public int ShoppingCartId { get; set; }  // Foreign key
        public ShoppingCart ShoppingCartAtThisVendor { get; set; } = null;
        public int TotalPrice { get; set; }

        public VendorVisit() { } // Required for EF Core

        public VendorVisit(Vendor vendor, ShoppingCart shoppingCart, int totalPrice)
        {
            Vendor = vendor;
            ShoppingCartAtThisVendor = shoppingCart;
            TotalPrice = totalPrice;
        }
    }
}
