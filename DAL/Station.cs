/* Ester Shmuel ID:318968468 && Orly Sellem ID:315208728
  Lecturer: Adina Milston
  Course: .Dot Net
  Exercise: 2
  The purpose of this file is to define station's entity
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
        public struct Station
        {
            public int Id { get; set; }

            public int Name { get; set; }

            public double Longitude { get; set; }

            public double Lattitude { get; set; }

            public int ChargeSlots {get; set;}

            public override string ToString()
            {
                return string.Format("\nId is:{0}\nName is:{1}\nLongitude is:{2}\nLattitude is:{3}\nChargeSlots is:{4}\n", Id, Name, Longitude, Lattitude, ChargeSlots);
            }

       
        }
    }
}

