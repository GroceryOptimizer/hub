using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class StockItem
    {
        public Product _product { get; }
        public int _ammount { get; }

        public StockItem(Product product, int ammount)
        {
            this._product = product;
            this._ammount = ammount;
        }

    }
}
