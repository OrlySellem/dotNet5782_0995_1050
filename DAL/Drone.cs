/* Ester Shmuel ID:318968468 && Orly Sellem ID:315208728
  Lecturer: Adina Milston
  Course: .Dot Net
  Exercise: 2
  The purpose of this file is to define drone's entity
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
        public struct Drone
        {
           public int Id { get; set; }

           public string Model { get; set; }

           public WeightCategories MaxWeight { get; set; }

            public DroneStatuses Status { get; set; }

            public double Battery { get; set; }

            public override string ToString()
            {
                return string.Format("\nId is:{0}\nModel is:{1}\nMaxWeight is:{2}\nStatus is:{3}\nBattery is:{4}\n", Id, Model, MaxWeight, Status, Battery);
            }
        }
    }
    
}
