using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Location
        {

            public double Longitude { get; set; }

            public double Lattitude { get; set; }

            public override string ToString()
            {
                return string.Format("Longitude is:{0}\nLattitude is:{1}\n", Longitude, Lattitude);
            }

        }
    }
   
}
