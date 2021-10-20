using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                return string.Format("Id is:{0}\t  Name is:{1}\t  Phone is:{2}\t  Longitude is:{3}\t Lattitude is:{4}\t", Id, Name, Phone, Longitude, Lattitude);
            }
        }
    }
}
}
