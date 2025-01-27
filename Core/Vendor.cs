using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    internal class Vendor
    {
        private string _name { get; }
        private Coordinates _coordinates { get; }

        public Vendor(string name, Coordinates coordinates)
        {
            this._name = name;
            this._coordinates = coordinates;
        }
    }
}
