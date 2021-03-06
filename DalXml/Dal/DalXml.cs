using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;
using DalApi;
using DO;


namespace Dal
{
    sealed class DalXml : IDal
    {
        #region singelton

        static readonly IDal instance = new DalXml();
        public static IDal Instance { get => instance; }
        DalXml() {}

        #endregion singelton
        /// <summary>
        /// Pointers to the files
        /// </summary>
        string customerPath = @"..\..\xml\CustomerXml.xml";//XElement
        string dronePath = @"..\..\xml\DroneXml.xml";  //XElement
        string droneChargePath = @"..\..\xml\DroneChargeXml.xml";//XElement
        string parcelPath = @"..\..\xml\ParcelXml.xml";//XMLSerializer
        string stationPath = @"..\..\xml\Station.xml";//XMLSerializer
        string dalConfigPath = @"..\..\xml\DalXmlConfig.xml";  //XElement

        #region ADD
        /// <summary>
        /// Add station to system (save in station's file)
        /// </summary>
        /// <param name="stationToAdd"></param>
        public void addStaion(Station stationToAdd)
        {
            var listStation = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);//load the list from the station's file
            if (listStation.Exists(x => x.Id == stationToAdd.Id)) //find if the station already axist
                throw new AlreadyExistException("station");

            listStation.Add(stationToAdd);
            XMLTools.SaveListToXMLSerializer(listStation, stationPath);//save the list with new station in station's file

        }

        /// <summary>
        /// Add drone to system (save in drone's file)
        /// </summary>
        /// <param name="DroneToAdd"></param>
        public void addDrone(Drone DroneToAdd)
        {

            var listDrone = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
            if (listDrone.Exists(x => x.Id == DroneToAdd.Id))
                throw new AlreadyExistException("drone");

            listDrone.Add(DroneToAdd);
            XMLTools.SaveListToXMLSerializer(listDrone, dronePath);
        }

        /// <summary>
        /// Add customer to system (save in customer's file)
        /// </summary>
        /// <param name="CustomerToAdd"></param>
        public void addCustomer(Customer CustomerToAdd)
        {
            var listCustomer = XMLTools.LoadListFromXMLSerializer<Customer>(customerPath);
            if (listCustomer.Exists(x => x.Id == CustomerToAdd.Id))
                throw new AlreadyExistException("customer");

            listCustomer.Add(CustomerToAdd);
            XMLTools.SaveListToXMLSerializer(listCustomer, customerPath);

        }

        /// <summary>
        /// Add parcel to system (save in parcel's file)
        /// </summary>
        /// <param name="ParcelToAdd"></param>
        public void addParcel(Parcel ParcelToAdd)
        {     
            
            var listParcel = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            if (listParcel.Exists(x => x.Id == ParcelToAdd.Id))
                throw new AlreadyExistException("parcel");

            if (ParcelToAdd.Id != 0) 
            {
                ParcelToAdd.Id = ParcelToAdd.Id;
            }
            else //if the parcel is new, the id is the runner number and we will update it
            {
                var dalConfig = XMLTools.LoadListFromXMLSerializer<string>(dalConfigPath);
                int num = int.Parse(dalConfig[5]);
                ParcelToAdd.Id = num++;
                dalConfig[5]=num.ToString();
                XMLTools.SaveListToXMLSerializer<string>(dalConfig, dalConfigPath);
            }

            listParcel.Add(ParcelToAdd);
            XMLTools.SaveListToXMLSerializer(listParcel, parcelPath);
        }

        #endregion ADD

        #region GET 
        /// <summary>
        /// Get parcel from the parcel's file
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel getParcel (int id)
        {
            var listParcel = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            Parcel parcel = (from findparcel in listParcel
                             where findparcel.Id == id
                             select findparcel).FirstOrDefault();
            if (parcel.Equals(default(Parcel)))
                throw new DoesntExistentObjectException("parcel");
            return parcel;

            //  throw new NotImplementedException();
        }

        /// <summary>
        /// Get drone from the drone's file
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone getDrone(int id)
        {
            var listDrone = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
            Drone drone = (from findDrone in listDrone
                           where findDrone.Id == id
                           select findDrone).FirstOrDefault();

            if (drone.Equals(default(Drone)))
                throw new DoesntExistentObjectException("drone");
            return drone;

        }

        /// <summary>
        /// Get the station from the station's file
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station getStation(int id)
        {
            var listStation = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            Station station = (from findStation in listStation
                               where findStation.Id == id
                               select findStation).FirstOrDefault();

            if (station.Equals(default(Station)))
                throw new DoesntExistentObjectException("station");
            return station;

        }

        /// <summary>
        /// Get customer from the customer's file
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer getCustomer(int id)
        {
            var listCustomer = XMLTools.LoadListFromXMLSerializer<Customer>(customerPath);

            Customer customer = (from findCustomer in listCustomer
                                 where findCustomer.Id == id
                                 select findCustomer).FirstOrDefault();

            if (customer.Equals(default(Customer)))//check if the custome is defult
                throw new DoesntExistentObjectException("customer");
            return customer;

        }

        /// <summary>
        /// Get drone charge from the file
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneCharge getDroneCharge(int id)
        {
            var listDroneCharge = XMLTools.LoadListFromXMLSerializer <DroneCharge> (droneChargePath);
            DroneCharge droneCharge = (from findDroneCharge in listDroneCharge
                                       where findDroneCharge.Droneld == id
                                       select findDroneCharge).FirstOrDefault();

            if (droneCharge.Equals(default(DroneCharge)))
                throw new chargingException("the drone is not charging");
            return droneCharge;

        }

        #endregion GET 

        #region DELETE
        /// <summary>
        /// Delete drone from the system (from drone's file)
        /// </summary>
        /// <param name="droneToDel"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void delFromDrones(Drone droneToDel)
        {
            //מחפשים האם יש חבילה שמשוייכת לרחפן לפני שמוחקים את הרחפן
            var listParcel = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            Parcel parcelToUpdate = listParcel.Find(x => x.Droneld == droneToDel.Id);
            if (parcelToUpdate.Id != 0)
            {
                //מעדכנת שאין לה רחפן משוייך
                if (parcelToUpdate.Delivered == null)
                {
                    delFromParcels(parcelToUpdate);
                    parcelToUpdate.Scheduled = null;
                    parcelToUpdate.PickedUp = null;
                    parcelToUpdate.Droneld = 0;
                    addParcel(parcelToUpdate);
                }
            }
            //מוחקת את הרחפן מהרשימה ומעדכנת בקובץ
            var listDrone = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
            listDrone.Remove(droneToDel);
            XMLTools.SaveListToXMLSerializer(listDrone, dronePath);

        }

        /// <summary>
        ///  Delete station from the system (from station's file)
        /// </summary>
        /// <param name="stationToDel"></param>
        /// <param name="freeDrone"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void delFromStations(Station stationToDel, bool freeDrone)
        {
            if (freeDrone) //אם יש רחפנים בטעינה
            {
                //נמצא את הרחפנים שבטעינה ונשחרר אותם
                var listDronesCharge = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneChargePath);
                foreach (var dronesCharge in listDronesCharge)
                {
                    if (dronesCharge.Stationld == stationToDel.Id)
                    {
                        Drone droneToUpdate = getDrone(dronesCharge.Droneld);
                        freeDroneCharge(droneToUpdate);
                    }
                }
            }
            var listStation = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            listStation.Remove(stationToDel);
            XMLTools.SaveListToXMLSerializer(listStation, stationPath);

        }

        /// <summary>
        ///  Delete parcel from the system (from parcel's file)
        /// </summary>
        /// <param name="parcelToDel"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void delFromParcels(Parcel parcelToDel)
        {
            XElement parcelList = XMLTools.LoadListFromXMLElement(parcelPath);

            var DelParcel = (from parcel in parcelList.Elements()
                             where (parcel.Element("Id").Value == parcelToDel.Id.ToString())
                             select parcel).FirstOrDefault();

            if (DelParcel == null)
                throw new DoesntExistentObjectException("parcel");

            DelParcel.Remove();
            XMLTools.SaveListToXMLElement(parcelList, parcelPath);
        }

        /// <summary>
        /// Delete customer from the system (from customer's file)
        /// </summary>
        /// <param name="customerToDel"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void delFromCustomers(Customer customerToDel)
        {

            var listCustomer = XMLTools.LoadListFromXMLSerializer<Customer>(customerPath);

            listCustomer.Remove(customerToDel);

            XMLTools.SaveListToXMLSerializer(listCustomer, customerPath);

        }

        /// <summary>
        /// Delete droneCharge from the system (from droneCharge's file)
        /// </summary>
        /// <param name="droneCharge"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void delFromChargingDrone(DroneCharge droneCharge)
        {
            // טוען את רשימת התחנות ומחפש את התחנה שבו הרחפן נטען, מגדיל את מספר עמדות הטעינה הפנויות בתחנה
            //ומוחק את החיבור בין התחנה לרחפן - כלומר את droneCharge
            var listStation = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            Station stationToUpdate = listStation.Find(x => x.Id == droneCharge.Stationld);
            listStation.Remove(stationToUpdate);
            XMLTools.SaveListToXMLSerializer(listStation, stationPath);
            plusChargeSlots(ref stationToUpdate);         
            addStaion(stationToUpdate);


            var listDroneCharge = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneChargePath);
            listDroneCharge.Remove(droneCharge);
            XMLTools.SaveListToXMLSerializer(listDroneCharge, droneChargePath);
        }

        #endregion DELETE

        #region GET_LIST
        /// <summary>
        /// Get parcels
        /// </summary>
        /// <param name="prdicat"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> getParcels(Predicate<Parcel> prdicat = null)
        {
            var listParcel = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            return listParcel;
        }

        /// <summary>
        /// Get drone charge's list
        /// </summary>
        /// <param name="prdicat"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> getDronesCharge(Predicate<DroneCharge> prdicat = null)
        {
            var listDroneCharge = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneChargePath);
            return listDroneCharge;
        }

        /// <summary>
        /// Get drones 
        /// </summary>
        /// <param name="prdicat"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> getDrones(Predicate<Drone> prdicat = null)
        {
            var listDrone = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
            return listDrone;
        }

        /// <summary>
        /// Get stations
        /// </summary>
        /// <param name="prdicat"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> getStations(Predicate<Station> prdicat = null)
        {
            var listStation = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            return listStation;
        }

        /// <summary>
        /// get customers
        /// </summary>
        /// <param name="prdicat"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> getCustomers(Predicate<Customer> prdicat = null)
        {
            var listCustomer = XMLTools.LoadListFromXMLSerializer<Customer>(customerPath);
            return listCustomer;
        }

        /// <summary>
        /// return the list of parcels that doesnt assign to drone
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> unconnected_parcels_to_Drone()
        {
            List<Parcel> parcelsList = new List<Parcel>();
            var listParcel = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);

            foreach (Parcel parcel in listParcel)
            {
                if (parcel.Droneld == 0 && parcel.Scheduled == null)
                    parcelsList.Add(parcel);
            }
            return parcelsList;
        }

        /// <summary>
        /// return the list of stations with free charge slots
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> stations_with_freeDroneCharge()
        {
            List<Station> stationList = new List<Station>();

            var listStation = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            foreach (Station station in listStation)
            {
                if (station.ChargeSlots > 0)
                    stationList.Add(station);
            }

            return stationList;
        }

        #endregion GET_LIST

        #region updateChargeSlots
        /// <summary>
        /// reduce the charge slots in station
        /// </summary>
        /// <param name="s"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void reduceChargeSlots(ref Station s)
        {
            if (s.ChargeSlots == 0)
                throw new chargingException("There isn't any available charging slots in this station");
            s.ChargeSlots--;//Reduce the number of claim positions

        }

        /// <summary>
        ///  plus the charge slots in station
        /// </summary>
        /// <param name="s"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void plusChargeSlots(ref Station s)
        {
            s.ChargeSlots++;
        }

        #endregion updateChargeSlots

        #region UPDATE
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void assign_drone_parcel(Drone droneToUpdate, Parcel parcelToUpdate)
        {

            var listParcel = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);

            Parcel myParcel = listParcel.Find(x => x.Id == parcelToUpdate.Id);
            listParcel.Remove(parcelToUpdate);

            myParcel.Droneld = droneToUpdate.Id;
            myParcel.Scheduled = DateTime.Now;

            listParcel.Add(myParcel);
            XMLTools.SaveListToXMLSerializer(listParcel, parcelPath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void drone_pick_parcel(Drone droneToUpdate, Parcel parcelToUpdate)
        {
            var listParcel = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);

            Parcel myParcel = listParcel.Find(x => x.Id == parcelToUpdate.Id);

            listParcel.Remove(parcelToUpdate);

            myParcel.PickedUp = DateTime.Now;

            listParcel.Add(myParcel);
            XMLTools.SaveListToXMLSerializer(listParcel, parcelPath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void delivery_arrive_toCustomer(Parcel parcelToUpdate, DateTime Delivered)
        {
            try
            {
                var listParcel = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);

                Parcel myParcel = listParcel.Find(x => x.Id == parcelToUpdate.Id);

                listParcel.Remove(parcelToUpdate);

                myParcel.Delivered = Delivered;

                listParcel.Add(myParcel);
                XMLTools.SaveListToXMLSerializer(listParcel, parcelPath);

            }
            catch (DoesntExistentObjectException ex)
            {
                throw new DoesntExistentObjectException(ex.Message);
            }

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void chargingDrone(Drone droneToUpdate, Station stationToUpdate)
        {
            try
            {
                getDrone(droneToUpdate.Id); //to check if the drone exist in the list

                var listStation = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

                var listChargeSlots = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneChargePath);

                Station myStation = listStation.Find(x => x.Id == stationToUpdate.Id);

                if (myStation.ChargeSlots != 0)//If there are no charge slots available, choose a new station  
                {
                    listStation.Remove(stationToUpdate);
                    reduceChargeSlots(ref myStation);
                    listStation.Add(myStation);
                    XMLTools.SaveListToXMLSerializer(listStation, stationPath);


                    DroneCharge droneChargeToAdd = new DroneCharge() { Droneld = droneToUpdate.Id, Stationld = myStation.Id, StartedRecharged = DateTime.Now };
                    listChargeSlots.Add(droneChargeToAdd);
                    XMLTools.SaveListToXMLSerializer(listChargeSlots, droneChargePath);

                    return;
                }
            }
            catch (DoesntExistentObjectException ex)
            {
                throw new DoesntExistentObjectException(ex.Message);
            }

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void freeDroneCharge(Drone droneToUpdate)
        {
            try
            {
                var listDroneCharge = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneChargePath);

                DroneCharge droneChargeToDel = listDroneCharge.Find(x => x.Droneld == droneToUpdate.Id);

                if (droneChargeToDel.Droneld != 0 && droneChargeToDel.Stationld != 0)
                {
                    delFromChargingDrone(droneChargeToDel);

                    return;
                }

                throw new chargingException("The drone doesn't charging in this station");
            }
            catch (DoesntExistentObjectException ex)
            {

                throw new DoesntExistentObjectException(ex.Message);
            }
        }
        #endregion UPDATE

        #region power
        [MethodImpl(MethodImplOptions.Synchronized)]
        public double[] R_power_consumption_Drone()
        {
            var dalConfig = XMLTools.LoadListFromXMLSerializer<string>(dalConfigPath);
            double.Parse(dalConfig[5]);

            double[] power = new double[5];
            power[0] = double.Parse(dalConfig[0]); ;
            power[1] = double.Parse(dalConfig[1]); ;
            power[2] = double.Parse(dalConfig[2]); ;
            power[3] = double.Parse(dalConfig[3]); ;
            power[4] = double.Parse(dalConfig[4]); ;
            return power;
        }


        #endregion power
    }


}