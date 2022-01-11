using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Drone
    {
        public int Id { get; set; }

        public string Model { get; set; }

        public WeightCategories MaxWeight { get; set; }

        public int Battery { get; set; }

        public DroneStatuses Status { get; set; }

        public ParcelInTransfer Parcel { get; set; }

        public Location CurrentLocation { get; set; }

        public override string ToString()
        {
            return string.Format("\nId is:{0}\nModel is:{1}\nMaxWeight is:{2}\nBattery is:{3}\nStatus is:{4}\nParcel in transfer is:{5}\nThe current location is:{6}\n", Id, Model, MaxWeight, Battery, Status, Parcel, CurrentLocation);
        }
    }
}
