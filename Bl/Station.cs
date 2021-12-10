using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Station
        {
            public int Id { get; set; }

            public int Name { get; set; }

            public Location Address { get; set; }

            public int ChargeSlots { get; set; }

            public List <ChargingDrone> Charging_drones { get; set; }

            public override string ToString()
            {
                return string.Format("\nId is:{0}\nName is:{1}\nAddress is:{2}\nThe number of charge slots is:{3}\nThe list of the charging drones is:{4}\n", Id, Name, Address, ChargeSlots, String.Join("\t", Charging_drones));
            }

        }
    }
 
}
