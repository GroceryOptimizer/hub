using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    internal class VendorVisit
    {
        private Vendor _vendor { get; }
        private Coordinates _vendorCoordinates {  get; }
        private ShoppingCart _shoppingCartAtThisVendor { get; }
        private int _totalPrice {  get; }

        public VendorVisit(Vendor vendor, Coordinates coordinates, ShoppingCart shoppingCart, int totalPrice)
        {
            this._vendor = vendor;
            this._vendorCoordinates = coordinates;
            this._shoppingCartAtThisVendor = shoppingCart;
            this._totalPrice = totalPrice;
        }

    }
}
