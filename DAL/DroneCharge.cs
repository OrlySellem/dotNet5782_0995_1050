/* Ester Shmuel ID:318968468 && Orly Sellem ID:315208728
  Lecturer: Adina Milston
  Course: .Dot Net
  Exercise: 2
  The purpose of this file is to droneCharge's entity
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct DroneCharge
        {
            public int Droneld { get; set; }

            public int Stationld { get; set; }

            public override string ToString()
            {
                return string.Format("\nDroneld is:{0}\nStationld is:{1}\n", Droneld, Stationld);
            }
        }
    }
}

