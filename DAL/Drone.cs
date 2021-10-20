﻿using System;
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
                return string.Format("Id is:{0}\t  Model is:{1}\t  MaxWeight is:{2}\t  Status is:{3}\t Battery is:{4}\t", Id, Model, MaxWeight, Status, Battery);
            }
        }
    }
    
}
