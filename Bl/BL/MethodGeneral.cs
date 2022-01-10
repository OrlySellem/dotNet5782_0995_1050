using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
using BO;
using BlApi;


namespace BL
{  
   partial class BL : IBL
    {
        //  
        //יתחזק רשימת רחפנים
        public static List <DroneToList> drones;

        //public static List<ChargingDrone> chargingDrones;

        private static double available;
        private static double lightWeight;//Lightweight issue
        private static double mediumWeight;//MediumWeight issue
        private static double heavyWeight;//Heavyweight issue
        private static double Drone_charging_speed;//Drone charging speed in percentage per hour  

        private static Random rand;

        /// <summary>
        /// Template of singleton
        /// </summary>
        #region singelton 
        static readonly BL instance = new BL();
        public static BL Instance { get => instance;  }
        static BL() { }

        internal readonly DalApi.IDal dal = DalFactory.GetDal();  //object of dal

        #endregion singelton

        #region constructor BL
        BL()//ctor of BlApi
        {
            rand = new Random();
           
            try
            {
                double minDistance = 0;

                dal = DalApi.DalFactory.GetDal();

                drones = new List<DroneToList>();

                double[] power = dal.R_power_consumption_Drone();
                available = power[0];
                lightWeight = power[1];
                mediumWeight = power[2];
                heavyWeight = power[3];
                Drone_charging_speed = power[4];

                //is must to be list because we want to use the index
                List<DO.Drone> dronesFromDS = dal.getDrones().ToList();
                List<DO.Parcel> parcelsFromDS = dal.getParcels().ToList();
                List<DO.Station> stationFromDS = dal.getStations().ToList();

           

                foreach (DO.Drone itemDrone in dronesFromDS)//pass over the drones's list 
                {
                    bool droneAssignToParcel = false;
                    DroneToList tempDroneToList = new DroneToList() //copy the data of IDAL.DO.drone to BlApi.BO.droneToLIst
                    {
                        Id = itemDrone.Id,

                        Model = itemDrone.Model,

                        MaxWeight = (BO.WeightCategories)itemDrone.MaxWeight,

                    };
                    #region case: the drone is assign to parcel
                    foreach (DO.Parcel itemParcel in parcelsFromDS)//pass over the parcels's list - לבקשת התרגיל 
                    {
                        //אם החבילה שויכה אך לא סופקה ולא נאספה 
                        if (itemDrone.Id == itemParcel.Droneld)//The drone is assign to parcel
                        {
                            droneAssignToParcel = true;
                            if (itemParcel.Delivered == null)//The parcel isn't delivered
                            {
                                tempDroneToList.Status = DroneStatuses.delivery;
                                tempDroneToList.idParcel = itemParcel.Id;
                                if (itemParcel.PickedUp == null)//The parcel isn't pick up from the station
                                {

                                    tempDroneToList.CurrentLocation = nearStationToCustomer(itemParcel.Senderld, ref minDistance);//מיקום הרחפן יהיה בתחנה הקרובה לשולח 
                                }
                                else //If the parcel is picked up but doesn't delivered
                                {
                                    tempDroneToList.CurrentLocation = new Location()//מיקום הרחפן יהיה במיקום השולח
                                    {
                                        Lattitude = dal.getCustomer(itemParcel.Senderld).Lattitude,
                                        Longitude = dal.getCustomer(itemParcel.Senderld).Longitude
                                    };
                                }

                                if ((BO.WeightCategories)itemParcel.Weight == BO.WeightCategories.light)
                                {
                                    double powerForDistance = power[1] * minDistance/Math.Pow(10,3);
                                    tempDroneToList.Battery = (int)(rand.NextDouble() * (100 - powerForDistance) + powerForDistance);
                                }

                                if ((BO.WeightCategories)itemParcel.Weight == BO.WeightCategories.medium)
                                {
                                    double powerForDistance = power[2] * minDistance / Math.Pow(10, 3);
                                   tempDroneToList.Battery = (int)(rand.NextDouble() * (100 - powerForDistance) + powerForDistance);

                                }

                                if ((BO.WeightCategories)itemParcel.Weight == BO.WeightCategories.heavy)
                                {

                                    double powerForDistance = power[3] * minDistance / Math.Pow(10, 3);
                                    tempDroneToList.Battery = (int)(rand.NextDouble() * (100 - powerForDistance) + powerForDistance);

                                }
                      
                                drones.Add(tempDroneToList);
                                break;
                            }

                        }
                    }
                    #endregion

                    #region case: drone ins't assign to parcel
                    if (!droneAssignToParcel)//if the drone isn't assign to parcel
                    {
                        tempDroneToList.Status = (DroneStatuses)rand.Next(1);//decide randomly between available or maintenance 

                        //	אם הרחפן בתחזוקה
                        if (tempDroneToList.Status == DroneStatuses.maintenance)
                        {
                            int indexStation = rand.Next(stationFromDS.Count());

                            tempDroneToList.CurrentLocation = new Location()
                            {
                                Lattitude = stationFromDS[indexStation].Lattitude,
                                Longitude = stationFromDS[indexStation].Longitude
                            };

                            tempDroneToList.Battery = rand.Next(20);

                            drones.Add(tempDroneToList);

                            dal.chargingDrone(itemDrone, stationFromDS[indexStation]);
                            break;

                        }

                        if (tempDroneToList.Status == DroneStatuses.available)
                        {
                            //עשיתי רנדום על החבילות לקחתי את הלקוח של היעד ואת המיקום שלו אני אכניס 
                            int indexParcel = rand.Next(parcelsFromDS.Count());
                            var customerLocation = dal.getCustomer(parcelsFromDS[indexParcel].Targetld);
                            tempDroneToList.CurrentLocation = new Location()
                            {
                                Lattitude = customerLocation.Lattitude,
                                Longitude = customerLocation.Longitude
                            };

                            minDistance = 0;
                           BO.Station nearStation = nearStationToDrone(tempDroneToList, ref minDistance);
                            double powerForDistance = power[0] * minDistance;
                            tempDroneToList.Battery = rand.NextDouble() * (100 - powerForDistance) + powerForDistance;

                        }

                        drones.Add(tempDroneToList);
                        break;
                    }
                }
            }
            #endregion
            catch (DO.DoesntExistentObjectException ex)
            {
                throw new DO.DoesntExistentObjectException(ex.Message);
            }
            catch (DO.AlreadyExistException ex)
            {
                throw new DO.AlreadyExistException(ex.Message);
            }
            catch(DO.stringException)
            {
                throw new string_Exception();
            }


        }
        #endregion constructor BL

        #region find nearest station to Drone
       BO.Station nearStationToDrone(DroneToList droneItem, ref double minDistance)
        {
            double checkDistance;
            IEnumerable<DO.Station> listStation = dal.getStations().ToList();
            DO.Station minStation;

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

            return new BO.Station()
            {
                Id = minStation.Id,
                Name = minStation.Name,
                Address = address,
                ChargeSlots = minStation.ChargeSlots,
            };
        }
        #endregion find nearest station to Drone

        #region find nearest station to customer

        Location nearStationToCustomer(int costumerId, ref double minDistance)
        {
            double checkDistance;

            IEnumerable<DO.Station> listStation = dal.getStations().ToList();

            DO.Station minStation;

            var costumerItem = dal.getCustomer(costumerId);

            //מרחק ראשון בין הלקוח המבוקש והמקום הראשון ברשימת התחנות
            minDistance = Math.Sqrt(Math.Pow(costumerItem.Lattitude - listStation.First().Lattitude, 2) + Math.Pow(costumerItem.Longitude - listStation.First().Longitude, 2));
            minStation = listStation.First();
            foreach  (var itemStation in listStation)
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
        #endregion find nearest station to customer
        public void openSimulator(int idDrone, Action update, Func<bool> checkStop)
        {
            Simulator(this, idDrone, update, checkStop);
        }
    }

}




