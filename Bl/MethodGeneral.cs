using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using.System.Device.Location;
using IBL.BO;

namespace IBL
{
    public partial class BL : IBL
    {
        IDAL.IDal dal;
        //○	אובייקט BLimp יתחזק רשימת רחפנים (ע"פ הישות הלוגיות "רחפן לרשימה")
        public static List <DroneToList> drones;

        public static List <ChargingDrone> chargingDrones;

        internal static double available;
        internal static double Lightweight;//Lightweight issue
        internal static double MediumWeight;//MediumWeight issue
        internal static double Heavyweight;//Heavyweight issue
        internal static double Drone_charging_speed;//Drone charging speed in percentage per hour

        public Location distance(Location costumerLocation)
        {
            List <IDAL.DO.Station> tempStation = dal.getAllStation();
            
            double Min= Math.Sqrt((costumerLocation.Lattitude- tempStation.)
            
           
            foreach (Station itemStation in tempStation)
            {
                if()
            }

        }



        public BL()
        {
            dal = new DalObject.DalObject();

            drones = new List <DroneToList> ();

            chargingDrones = new List <ChargingDrone> ();

            List < IDAL.DO.Drone > dronesFromDS = dal.getAllDrones();
            List <IDAL.DO.Parcel> parcelsFromDS = dal.getAllParcels();

            foreach (IDAL.DO.Drone itemDrone in dronesFromDS)
            {
                DroneToList temp = new DroneToList()
                {
                    Id = itemDrone.Id,

                    Model = itemDrone.Model,

                    MaxWeight = (BO.WeightCategories)itemDrone.MaxWeight,

                    Battery = itemDrone.Battery
                };

                foreach (IDAL.DO.Parcel itemParcel in parcelsFromDS)
                {
                    //אם החבילה שויכה אך לא סופקה ולא נאספה 
                    if (itemDrone.Id == itemParcel.Droneld)//The drone is assign to parcel
                    {
                        if (itemParcel.Delivered == new DateTime(01, 01, 0001))//The parcel isn't delivered
                        {
                            temp.Status = DroneStatuses.delivery;

                            if(itemParcel.PickedUp == new DateTime(01, 01, 0001))
                            {
                                 


                            }
                        }

                    }    

                }

               

                drones.Add(temp);
            }

           
                
            }







    }
          






        public void chargingDrone(int droneId)
        {

        }











    }
}
