﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class ChargingDrone
        {
            public int Id { get; set; }

            public double Battery { get; set; }

            public override string ToString()
            {
                return string.Format("Id is:{0}\nBattery is:{1}\n", Id, Battery);
            }

        }
    }
   
}