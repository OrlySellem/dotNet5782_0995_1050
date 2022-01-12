using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Station
    {
        /// <summary>
        /// station's id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// station's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Longitude in the globe 
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Lattitude in the globe 
        /// </summary>
        public double Lattitude { get; set; }

        /// <summary>
        /// The number of the available charge slots
        /// </summary>
        public int ChargeSlots { get; set; }

        public override string ToString()
        {
            return string.Format("\nId is:{0}\nName is:{1}\nLongitude is:{2}\nLattitude is:{3}\nChargeSlots is:{4}\n", Id, Name, Longitude, Lattitude, ChargeSlots);
        }


    }
}
