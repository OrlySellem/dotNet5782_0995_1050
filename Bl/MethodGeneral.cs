using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public partial class BL : IBL
    {
        internal static Random rand = new Random();
        IDAL.IDal dal; //object of dal
        //יתחזק רשימת רחפנים
        public static List<DroneToList> drones;

        public static List<ChargingDrone> chargingDrones;

        internal static double available;
        internal static double lightWeight;//Lightweight issue
        internal static double mediumWeight;//MediumWeight issue
        internal static double heavyWeight;//Heavyweight issue
        internal static double Drone_charging_speed;//Drone charging speed in percentage per hour

        Station nearStationToDrone(DroneToList droneItem, ref double minDistance)
        {
            double checkDistance;
            IEnumerable<IDAL.DO.Station> listStation = dal.getStations().ToList();
            IDAL.DO.Station minStation;

            minDistance = Math.Sqrt(Math.Pow(droneItem.CurrentLocation.Lattitude - listStation.First().Lattitude, 2) + Math.Pow(droneItem.CurrentLocation.Longitude - listStation.First().Longitude, 2));
            minStation = listStation.First();

            foreach (var itemStation in listStation)
            {
                checkDistance = Math.Sqrt(Math.Pow(droneItem.CurrentLocation.Lattitude - itemStation.Lattitude, 2) + Math.Pow(droneItem.CurrentLocation.Longitude - itemStation.Longitude, 2));
                if (minDistance > checkDistance && itemStation.ChargeSlots > 0)
                {
                    minDistance = checkDistance;
                    minStation = itemStation;
                }
            }
            if (minStation.ChargeSlots <= 0)
                throw new NoFreeChargingStations();

            Location address = new Location()
            {
                Lattitude = minStation.Lattitude,
                Longitude = minStation.Longitude
            };

            return new Station()
            {
                Id = minStation.Id,
                Name = minStation.Name,
                Address = address,
                ChargeSlots = minStation.ChargeSlots,
            };
        }

        Location nearStationToCustomer(int costumerId, ref double minDistance)
        {
            double checkDistance;

            IEnumerable<IDAL.DO.Station> listStation = dal.getStations().ToList();

            IDAL.DO.Station minStation;

            var costumerItem = dal.getCustomer(costumerId);

            //מרחק ראשון בין הלקוח המבוקש והמקום הראשון ברשימת התחנות
            minDistance = Math.Sqrt(Math.Pow(costumerItem.Lattitude - listStation.First().Lattitude, 2) + Math.Pow(costumerItem.Longitude - listStation.First().Longitude, 2));
            minStation = listStation.First();
            foreach (var itemStation in listStation)
            {
                checkDistance = Math.Sqrt(Math.Pow(costumerItem.Lattitude - itemStation.Lattitude, 2) + Math.Pow(costumerItem.Longitude - itemStation.Longitude, 2));
                if (minDistance > checkDistance)
                {
                    minDistance = checkDistance;
                    minStation = itemStation;
                }
            }
            Location returnTemp = new Location()
            {
                Lattitude = minStation.Lattitude,
                Longitude = minStation.Longitude
            };
            return returnTemp;
        }

        public BL()//ctor of BL
        {
            try
            {
                double minDistance = 0;

                dal = new DalObject.DalObject();

                drones = new List<DroneToList>();

               // chargingDrones = new List<ChargingDrone>();

                double[] power = dal.R_power_consumption_Drone();
                available = power[0];
                lightWeight = power[1];
                mediumWeight = power[2];
                heavyWeight = power[3];
                Drone_charging_speed = power[4];

                List<IDAL.DO.Drone> dronesFromDS = dal.getDrones().ToList();
                List<IDAL.DO.Parcel> parcelsFromDS = dal.getParcels().ToList();
                List<IDAL.DO.Station> stationFromDS = dal.getStations().ToList();

                foreach (IDAL.DO.Drone itemDrone in dronesFromDS)//pass over the drones's list 
                {
                    bool droneAssignToParcel = false;
                    DroneToList temp = new DroneToList() //copy the data of IDAL.DO.drone to IBL.BO.droneToLIst
                    {
                        Id = itemDrone.Id,

                        Model = itemDrone.Model,

                        MaxWeight = (BO.WeightCategories)itemDrone.MaxWeight,

                    };
                    #region case: the drone is assign to parcel
                    foreach (IDAL.DO.Parcel itemParcel in parcelsFromDS)//pass over the parcels's list - לבקשת התרגיל 
                    {
                        //אם החבילה שויכה אך לא סופקה ולא נאספה 
                        if (itemDrone.Id == itemParcel.Droneld)//The drone is assign to parcel
                        {
                            droneAssignToParcel = true;
                            if (itemParcel.Delivered == null)//The parcel isn't delivered
                            {
                                temp.Status = DroneStatuses.delivery;
                                temp.idParcel = itemParcel.Id;
                                if (itemParcel.PickedUp == null)//The parcel isn't pick up from the station
                                {

                                    temp.CurrentLocation = nearStationToCustomer(itemParcel.Senderld, ref minDistance);//מיקום הרחפן יהיה בתחנה הקרובה לשולח 
                                }
                                else //If the parcel is picked up but doesn't delivered
                                {
                                    temp.CurrentLocation = new Location()//מיקום הרחפן יהיה במיקום השולח
                                    {
                                        Lattitude = dal.getCustomer(itemParcel.Senderld).Lattitude,
                                        Longitude = dal.getCustomer(itemParcel.Senderld).Longitude
                                    };
                                }

                                if ((WeightCategories)itemParcel.Weight == WeightCategories.light)
                                {
                                    double powerForDistance = power[1] * minDistance/Math.Pow(10,3);
                                    temp.Battery = (int)(rand.NextDouble() * (100 - powerForDistance) + powerForDistance);                  
                                }

                                if ((WeightCategories)itemParcel.Weight == WeightCategories.medium)
                                {
                                    double powerForDistance = power[2] * minDistance / Math.Pow(10, 3);
                                    temp.Battery = (int)(rand.NextDouble() * (100 - powerForDistance) + powerForDistance);
                                }

                                if ((WeightCategories)itemParcel.Weight == WeightCategories.heavy)
                                {
                                    double powerForDistance = power[3] * minDistance / Math.Pow(10, 3);
                                    temp.Battery = (int)(rand.NextDouble() * (100 - powerForDistance) + powerForDistance);
                                }
                      
                                drones.Add(temp);
                                break;
                            }

                        }
                    }
                    #endregion

                    #region case: drone ins't assign to parcel
                    if (!droneAssignToParcel)//if the drone isn't assign to parcel
                    {
                        temp.Status = (DroneStatuses)rand.Next(1);//decide randomly between available or maintenance 

                        //	אם הרחפן בתחזוקה
                        if (temp.Status == DroneStatuses.maintenance)
                        {
                            int indexStation = rand.Next(stationFromDS.Count());

                            temp.CurrentLocation = new Location()
                            {
                                Lattitude = stationFromDS[indexStation].Lattitude,
                                Longitude = stationFromDS[indexStation].Longitude
                            };

                            temp.Battery = rand.Next(20);

                            drones.Add(temp);

                            dal.chargingDrone(itemDrone, stationFromDS[indexStation]);
                            break;

                        }

                        if (temp.Status == DroneStatuses.available)
                        {
                            //עשיתי רנדום על החבילות לקחתי את הלקוח של היעד ואת המיקום שלו אני אכניס 
                            int indexParcel = rand.Next(parcelsFromDS.Count());
                            var customerLocation = dal.getCustomer(parcelsFromDS[indexParcel].Targetld);
                            temp.CurrentLocation = new Location()
                            {
                                Lattitude = customerLocation.Lattitude,
                                Longitude = customerLocation.Longitude
                            };

                            minDistance = 0;
                            Station nearStation = nearStationToDrone(temp, ref minDistance);
                            double powerForDistance = power[0] * minDistance;
                            temp.Battery = rand.NextDouble() * (100 - powerForDistance) + powerForDistance;

                        }

                        drones.Add(temp);
                        break;
                    }
                }
            }
            #endregion
            catch (IDAL.DO.DoesntExistentObjectException ex)
            {
                throw new DoesntExistentObjectException(ex.Message);
            }
            catch (IDAL.DO.AlreadyExistException ex)
            {
                throw new AlreadyExistException(ex.Message);
            }


        }

    }
}




