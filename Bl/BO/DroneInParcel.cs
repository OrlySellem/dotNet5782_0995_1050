using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DroneInParcel
    {
        public int Id { get; set; }

        public double Battery { get; set; }

        public Location CurrentLocation { get; set; }

        public override string ToString()
        {
            return string.Format("id is:{0}\nBattery is:{1}\nCurrent location is:{3}", Id, Battery, CurrentLocation);
        }

    }
}
