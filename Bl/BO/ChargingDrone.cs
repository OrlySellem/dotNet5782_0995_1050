using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class ChargingDrone
    {
        /// <summary>
        /// Id of drone in charging 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The percent's battery of drone
        /// </summary>
        public double Battery { get; set; }

        public override string ToString()
        {
            return string.Format("\nId is:{0}\nBattery is:{1}\n", Id, Battery);
        }


    }
}