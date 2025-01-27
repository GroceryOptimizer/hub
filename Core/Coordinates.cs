using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    internal class Coordinates
    {
        private float _longitude { get; }
        private float _latitude { get; }

        public Coordinates(float longitude, float latitude)
        {
            this._longitude = longitude;
            this._latitude = latitude;
        }
    }
}
