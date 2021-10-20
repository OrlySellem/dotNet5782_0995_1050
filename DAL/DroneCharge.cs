using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    namespace DO
    {
        public struct DroneCharge
        {
            public int Droneld { get; set; }

            public int Stationld { get; set; }

            public override string ToString()
            {
                return string.Format("Droneld is:{0}\t  Stationld is:{1}\t", Droneld, Stationld);
            }
        }
    }
}

