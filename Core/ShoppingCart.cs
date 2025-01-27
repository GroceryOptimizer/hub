using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    internal class ShoppingCart
    {
        private StockItem[] _cart { get; }

        public ShoppingCart (StockItem[] cart)
        {
            this._cart = cart;
        }
    }
}
