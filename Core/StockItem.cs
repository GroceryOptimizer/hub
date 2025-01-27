using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    internal class StockItem
    {
        private Product _product { get; }
        private int _ammount { get; }

        public StockItem(Product product, int ammount)
        {
            this._product = product;
            this._ammount = ammount;
        }

    }
}
