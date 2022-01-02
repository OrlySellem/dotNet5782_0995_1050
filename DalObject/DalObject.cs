
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;


namespace Dal
{
    sealed class DalObject : IDal
    {

        #region singelton
        static readonly IDal instance = new DalObject();
        public static IDal Instance { get => instance; }
        DalObject() { }
        internal static Random rand = new Random();

        #endregion singelton

        #region ADD
        public void addStaion(Station stationToAdd)
        {
            Station temp = DataSource.stations.Find(x => x.Id == stationToAdd.Id);
            if (temp.Id != 0)
                throw new AlreadyExistException("station");

            DataSource.stations.Add(stationToAdd);
        }

        public void addDrone(Drone droneToAdd)
        {
            Drone temp = DataSource.drones.Find(x => x.Id == droneToAdd.Id);
            if (temp.Id != 0)
                throw new AlreadyExistException("drone");

            DataSource.drones.Add(droneToAdd);
        }

        public void addCustomer(Customer CustomerToAdd)
        {
            Customer temp = DataSource.customers.Find(x => x.Id == CustomerToAdd.Id);
            if (temp.Id != 0)
                throw new AlreadyExistException("customer");

            DataSource.customers.Add(CustomerToAdd);
        }
        //צריך שהתוכנית תגדיר לו ת"ז
        public void addParcel(Parcel ParcelToAdd)
        {
            Parcel temp = DataSource.parcels.Find(x => x.Id == ParcelToAdd.Id);
            if (temp.Id != 0)
                throw new AlreadyExistException("parcel");
            if (ParcelToAdd.Id != 0)
                ParcelToAdd.Id = ParcelToAdd.Id;
            else
            {
                ParcelToAdd.Id = DataSource.Config.idParcel++;
            }


            DataSource.parcels.Add(ParcelToAdd);
        }
        #endregion

        #region REMOVE
        public void delFromDrones(Drone droneToDel)
        {
            Parcel parcelToUpdate = DataSource.parcels.Find(x => x.Droneld == droneToDel.Id);
            if (parcelToUpdate.Id != 0)
            {
                if (parcelToUpdate.Delivered == null)
                {
                    delFromParcels(parcelToUpdate);
                    parcelToUpdate.Scheduled = null;
                    parcelToUpdate.PickedUp = null;
                    parcelToUpdate.Droneld = 0;
                    addParcel(parcelToUpdate);
                }
            }

            DataSource.drones.Remove(droneToDel);
        }

        public void delFromStations(Station stationToDel, bool freeDrone = true)
        {
            if (freeDrone)
            {
                foreach (var item in DataSource.dronesCharge)
                {
                    if (item.Stationld == stationToDel.Id)
                    {
                        Drone droneToUpdate = getDrone(item.Droneld);
                        freeDroneCharge(droneToUpdate);
                    }
                }
            }

            DataSource.stations.Remove(stationToDel);
        }

        public void delFromParcels(Parcel parcelToDel)
        {
            DataSource.parcels.Remove(parcelToDel);
        }

        public void delFromCustomers(Customer customerToDel)
        {
            DataSource.customers.Remove(customerToDel);
        }

        public void delFromChargingDrone(DroneCharge droneCharge)
        {
            Station stationToUpdate = DataSource.stations.Find(x => x.Id == droneCharge.Stationld);
            DataSource.stations.Remove(stationToUpdate);
            plusChargeSlots(ref stationToUpdate);
            addStaion(stationToUpdate);

            DataSource.dronesCharge.Remove(droneCharge);
        }

        #endregion

        #region GET 
        public Parcel getParcel(int id)//Finds the requested parcel from the arr
        {
            //Parcel parcel = DataSource.parcels.Find(p => p.Id == id);
            //דוצמא לשאילתה LINQ של קבלת הרחפן

            Parcel parcel = (from findparcel in DataSource.parcels
                             where findparcel.Id == id
                             select findparcel).FirstOrDefault();
            if (parcel.Id == 0)
                throw new DoesntExistentObjectException("parcel");
            return parcel;
        }

        public Drone getDrone(int id)//Finds the requested drone from the arr
        {
            Drone drone = DataSource.drones.Find(d => d.Id == id);
            if (drone.Id == 0)
                throw new DoesntExistentObjectException("drone");
            return drone;
        }

        public Station getStation(int id)//Finds the requested station from the arr
        {
            Station station = DataSource.stations.Find(s => s.Id == id);
            if (station.Id == 0)
                throw new DoesntExistentObjectException("station");
            return station;
        }

        public Customer getCustomer(int id)//loop to find the customer acordding to ID 
        {
            Customer customer = DataSource.customers.Find(c => c.Id == id);
            if (customer.Id == 0)
                throw new DoesntExistentObjectException("customer");
            return customer;
        }

        public DroneCharge getDroneCharge(int id)//loop to find the customer acordding to ID 
        {
            DroneCharge droneCharge = DataSource.dronesCharge.Find(c => c.Droneld == id);

            return droneCharge;
        }
        #endregion

        #region GET_LIST


        public IEnumerable<Parcel> getParcels(Predicate<Parcel> prdicat = null)
        {
            //return (from parcel in DataSource.parcels
            //        where prdicat(parcel)
            //        select parcel).ToList();
            return DataSource.parcels.FindAll(x => prdicat == null ? true : prdicat(x));


        }

        public IEnumerable<Drone> getDrones(Predicate<Drone> prdicat = null)
        {
            return DataSource.drones.FindAll(x => prdicat == null ? true : prdicat(x));
        }

        public IEnumerable<Station> getStations(Predicate<Station> prdicat = null)//return list of stations
        {
            return DataSource.stations.FindAll(x => prdicat == null ? true : prdicat(x));
        }

        public IEnumerable<Customer> getCustomers(Predicate<Customer> prdicat = null)//return list of stations
        {
            return DataSource.customers.FindAll(x => prdicat == null ? true : prdicat(x));
        }

        //public IEnumerable<Customer> getAllCustomer()//return list of customers
        //{
        //    return DataSource.customers;
        //}

        public IEnumerable<DroneCharge> getDronesCharge(Predicate<DroneCharge> prdicat = null)
        {
            return DataSource.dronesCharge.FindAll(x => prdicat == null ? true : prdicat(x));
        }

        public IEnumerable<Parcel> print_unconnected_parcels_to_Drone()
        {
            List<Parcel> parcelsList = new List<Parcel>();
            foreach (Parcel item in DataSource.parcels)
            {
                if (item.Droneld == 0 && item.Scheduled == null)
                    parcelsList.Add(item);
            }
            return parcelsList;
        }
        public IEnumerable<Station> print_stations_with_freeDroneCharge()
        {
            List<Station> stationList = new List<Station>();

            foreach (Station item in DataSource.stations)
            {
                if (item.ChargeSlots > 0)
                    stationList.Add(item);
            }

            return stationList;
        }

        #endregion GET_LIST

        #region updateChargeSlots
        public void reduceChargeSlots(ref Station s)
        {
            if (s.ChargeSlots == 0)
                throw new chargingException("There isn't any available charging slots in this station");
            s.ChargeSlots--;//Reduce the number of claim positions
        }

        public void plusChargeSlots(ref Station s)
        {
            s.ChargeSlots++;
        }
        #endregion

        #region update
        public void assign_drone_parcel(Drone droneToUpdate, Parcel parcelToUpdate)//assign drone to parcel
        {
            try
            {
                Parcel myParcel = getParcel(parcelToUpdate.Id);
                DataSource.parcels.Remove(parcelToUpdate);

                myParcel.Droneld = droneToUpdate.Id;
                myParcel.Scheduled = DateTime.Now;

                DataSource.parcels.Add(myParcel);
            }
            catch (DoesntExistentObjectException ex)
            {

                throw new DoesntExistentObjectException(ex.Message);
            }

        }

        public void drone_pick_parcel(Drone droneToUpdate, Parcel parcelToUpdate)//pick up parcel by drone
        {
            try
            {
                Drone myDrone = getDrone(droneToUpdate.Id);
                Parcel myParcel = getParcel(parcelToUpdate.Id);

                DataSource.drones.Remove(droneToUpdate);
                DataSource.parcels.Remove(parcelToUpdate);

                myParcel.PickedUp = DateTime.Now;

                DataSource.drones.Add(myDrone);
                DataSource.parcels.Add(myParcel);
            }
            catch (DoesntExistentObjectException ex)
            {
                throw new DoesntExistentObjectException(ex.Message);
            }


        }

        public void delivery_arrive_toCustomer(Parcel parcelToUpdate)//The delivery arrived to the customer
        {
            try
            {
                //getDrone(droneToUpdate.Id);
                Parcel myParcel = getParcel(parcelToUpdate.Id);

                DataSource.parcels.Remove(parcelToUpdate);

                myParcel.Delivered = DateTime.Now;

                DataSource.parcels.Add(myParcel);

            }
            catch (DoesntExistentObjectException ex)
            {
                throw new DoesntExistentObjectException(ex.Message);
            }

        }

        public void chargingDrone(Drone droneToUpdate, Station stationToUpdate)//Inserts a drone to charg
        {
            try
            {
                Drone myDrone = getDrone(droneToUpdate.Id);
                Station myStation = getStation(stationToUpdate.Id);

                if (myStation.ChargeSlots != 0)//If there are no charge slots available, choose a new station  
                {
                    DataSource.stations.Remove(stationToUpdate);
                    reduceChargeSlots(ref myStation);
                    DataSource.stations.Add(myStation);

                    DroneCharge droneChargeToAdd = new DroneCharge() { Droneld = myDrone.Id, Stationld = myStation.Id, StartedRecharged = DateTime.Now };
                    DataSource.dronesCharge.Add(droneChargeToAdd);
                    return;
                }

                throw new chargingException("The station doesn't have available charging slot");
            }
            catch (DoesntExistentObjectException ex)
            {

                throw new DoesntExistentObjectException(ex.Message);
            }

        }

        public void freeDroneCharge(Drone droneToUpdate)//Drone release from charging
        {
            try
            {
                DroneCharge droneChargeToDel = DataSource.dronesCharge.Find(x => x.Droneld == droneToUpdate.Id);

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
        #endregion

        #region power
        public double[] R_power_consumption_Drone()
        {
            double[] power = new double[5];
            power[0] = DataSource.Config.available;
            power[1] = DataSource.Config.lightWeight;
            power[2] = DataSource.Config.mediumWeight;
            power[3] = DataSource.Config.heavyWeight;
            power[4] = DataSource.Config.Drone_charging_speed;

            return power;
        }

        #endregion

    }

}

