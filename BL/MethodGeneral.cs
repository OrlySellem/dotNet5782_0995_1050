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
        internal static Random rand = new Random();
        IDAL.IDal dal;
        //○	אובייקט BLimp יתחזק רשימת רחפנים (ע"פ הישות הלוגיות "רחפן לרשימה")
        public static List <DroneToList> drones;

        public static List <ChargingDrone> chargingDrones;

        internal static double available;
        internal static double Lightweight;//Lightweight issue
        internal static double MediumWeight;//MediumWeight issue
        internal static double Heavyweight;//Heavyweight issue
        internal static double Drone_charging_speed;//Drone charging speed in percentage per hour

        public Location distance(int costumerId)
        {
            double checkDistance;

            List<IDAL.DO.Customer> listCustomer = dal.getAllCustomer();

            List<IDAL.DO.Station> listStation = dal.getAllStation();
            IDAL.DO.Station tempStation;

            var costumerItem=dal.getCustomer(costumerId);

                if (costumerItem.Id== costumerId)//מצאנו את השולח המבוקש עכשיו נמצא מרחק קטן
                {
                    //מרחק ראשון בין הלקוח המבוקש והמקום הראשון ברשימת התחנות
                    double Min = Math.Sqrt(Math.Pow(costumerItem.Lattitude - listStation[0].Lattitude, 2) + Math.Pow(costumerItem.Longitude - listStation[0].Longitude, 2));
                    tempStation = listStation[0];
                    foreach (var itemStation in listStation)
                    {
                        checkDistance = Math.Sqrt(Math.Pow(costumerItem.Lattitude - itemStation.Lattitude, 2) + Math.Pow(costumerItem.Longitude - itemStation.Longitude, 2));
                        if(Min > checkDistance)
                        {
                            Min = checkDistance;
                            tempStation = itemStation;
                        }
                    }
                    Location returnTemp = new Location()
                    {
                        Lattitude = tempStation.Lattitude,
                        Longitude = tempStation.Longitude
                    };
                  return returnTemp;
                }

            
            throw new customerException("doesn't exist in the system");

        }


        //מתי להכניס את הADD
        public BL()
        {

            dal = new DalObject.DalObject();

            drones = new List <DroneToList> ();

            chargingDrones = new List <ChargingDrone> ();

            List < IDAL.DO.Drone > dronesFromDS = dal.getAllDrones();
            List <IDAL.DO.Parcel> parcelsFromDS = dal.getAllParcels();
            List<IDAL.DO.Station> stationFromDS = dal.getAllStation();

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

                                temp.CurrentLocation = distance(itemParcel.Senderld);//נניח שהוא יוצר את המרחק ובודק את המרחק הקטן יותר
                                break;
                            }
                            temp.CurrentLocation = new Location()
                            {
                                Lattitude = dal.getCustomer(itemParcel.Senderld).Lattitude,
                                Longitude = dal.getCustomer(itemParcel.Senderld).Longitude
                            };

                            drones.Add(temp);

                            
                        }

                    }    

                }

                //	אם הרחפן לא מבצע משלוח
                temp.Status= (DroneStatuses)rand.Next(1);

                //	אם הרחפן בתחזוקה
                if (temp.Status== DroneStatuses.maintenance)
                {
                    int randStation = rand.Next(stationFromDS.Count());
                    temp.CurrentLocation = new Location()
                    {
                        Lattitude = stationFromDS[randStation].Lattitude,
                        Longitude = stationFromDS[randStation].Longitude
                    };

                    temp.Battery = rand.Next(20);

                    drones.Add(temp);

                }

                if (temp.Status == DroneStatuses.available)
                {
                    //עשיתי רנדום על החבילות לקחתי את הלקוח של היעד ואת המיקום שלו אני אכניס 
                    int randParcel = rand.Next(parcelsFromDS.Count());
                 var customerLocation=dal.getCustomer(parcelsFromDS[randParcel].Targetld);
                    temp.CurrentLocation = new Location()
                    {
                        Lattitude = customerLocation.Lattitude,
                        Longitude = customerLocation.Longitude
                    };

                    temp.Battery = rand.Next(20,100);
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
