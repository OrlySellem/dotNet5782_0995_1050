using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Station
    {
        /// <summary>
        /// The id of station
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of station
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The address of station
        /// </summary>
        public Location Address { get; set; }

        /// <summary>
        /// The number of free charging slots in station
        /// </summary>
        public int ChargeSlots { get; set; } 

        /// <summary>
        /// The drone's list in charging at this station
        /// </summary>
        public List <ChargingDrone> Charging_drones { get; set; }

        public override string ToString()
        {
            return string.Format("\nId is:{0}\nName is:{1}\nAddress is:{2}\nThe number of charge slots is:{3}\nThe list of the charging drones is:{4}\n", Id, Name, Address, ChargeSlots, String.Join("\t", Charging_drones));
        }

    }
}
