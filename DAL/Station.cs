using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Station
        {
            public int Id { get; set; }

            public int Name { get; set; }

            public double Longitude { get; set; }

            public double Lattitude { get; set; }

            public int ChargeSlots {get; set;}

            public override string ToString()
            {
                return string.Format("Id is:{0}\t  Name is:{1}\t  Longitude is:{2}\t  Lattitude is:{3}\t ChargeSlots is:{4}\t \n", Id, Name, Longitude, Lattitude, ChargeSlots);
            }
        }
    }
}

