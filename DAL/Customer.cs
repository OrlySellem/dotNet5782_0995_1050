using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;

namespace IDAL
{
    namespace DO
    {
        public struct Customer
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Phone { get; set; }

            public double Longitude { get; set; }

            public double Lattitude { get; set; }

            public override string ToString()
            {
            return string.Format("\nId is:{0}\nName is:{1}\nPhone is:{2}\nLongitude is:{3}\nLattitude is:{4}\n", Id, Name, Phone, Longitude, Lattitude);
            }
        }
    }
}

