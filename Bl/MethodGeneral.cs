﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public partial class BL : IBL
    {
        IDAL.IDal dal;
        //○	אובייקט BLimp יתחזק רשימת רחפנים (ע"פ הישות הלוגיות "רחפן לרשימה")
        public static List <DroneToList> drones;

        public static List <ChargingDrone> chargingDrones;

        public BL()
        {
            dal = new DalObject.DalObject();

            drones = new List <DroneToList> ();

            chargingDrones = new List <ChargingDrone> ();
        }

        public void chargingDrone(int droneId)
        {

        }











    }
}
