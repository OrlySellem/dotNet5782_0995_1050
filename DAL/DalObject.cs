/* Ester Shmuel ID:318968468 && Orly Sellem ID:315208728
  Lecturer: Adina Milston
  Course: .Dot Net
  Exercise: 2
  The purpose of DalObject's file is to manages all add / bring / update methods
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public class DalObject : IDAL.IDal
    {
        internal static Random rand = new Random();
        #region ctor
        public DalObject()
        {
            DataSource.Initialize();
        }
        #endregion

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

        public void addParcel(Parcel ParcelToAdd)
        {
            Parcel temp = DataSource.parcels.Find(x => x.Id == ParcelToAdd.Id);
            if (temp.Id != 0)
                throw new AlreadyExistException("parcel");

            temp.Id = DataSource.Config.idParcel++;
            DataSource.parcels.Add(ParcelToAdd);
        }
        #endregion

        #region REMOVE
        public void delFromDrones(Drone droneToDel)
        {
            Parcel parcelToUpdate = DataSource.parcels.Find(x => x.Droneld == droneToDel.Id);
            if (parcelToUpdate.Id!=0)
            {
                if (parcelToUpdate.Delivered == new DateTime(01, 01, 0001))
                {
                    delFromParcels(parcelToUpdate);
                    parcelToUpdate.Scheduled = new DateTime(01, 01, 0001);
                    parcelToUpdate.PickedUp = new DateTime(01, 01, 0001);
                    parcelToUpdate.Droneld = 0;
                    addParcel(parcelToUpdate);
                }
            }
             
            DataSource.drones.Remove(droneToDel);
        }

        public void delFromStations(Station stationToDel, bool freeDrone=true)
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

        public void delFromParcels (Parcel parcelToDel)
        {
            DataSource.parcels.Remove(parcelToDel);
        }

        public void delFromCustomers (Customer customerToDel)
        {
            DataSource.customers.Remove(customerToDel);
        }

        public void delFromChargingDrone (DroneCharge droneCharge)
        {
            Station stationToUpdate = DataSource.stations.Find (x => x.Id == droneCharge.Stationld);
            DataSource.stations.Remove(stationToUpdate);
            plusChargeSlots(ref stationToUpdate);
            addStaion(stationToUpdate);

            DataSource.dronesCharge.Remove(droneCharge);
        }

        #endregion

        #region GET 
        public Parcel getParcel(int id)//Finds the requested parcel from the arr
        {
            Parcel parcel = DataSource.parcels.Find(p => p.Id == id);
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
        #endregion

        #region GET_LIST
        public IEnumerable<Parcel> getAllParcels()//Return list of parcels
        {
            return DataSource.parcels;
        }

        public IEnumerable<Drone> getAllDrones()//Return list of drones
        {
            return DataSource.drones;
        }


        public IEnumerable<Station> getAllStation()//return list of stations
        {
            return DataSource.stations;
        }

        public IEnumerable<Customer> getAllCustomer()//return list of customers
        {
            return DataSource.customers;
        }

        public IEnumerable<DroneCharge> getAllDroneCharge()
        {
            return DataSource.dronesCharge;
        }

        public IEnumerable<Parcel> print_unconnected_parcels_to_Drone()
        {
         List<Parcel> parcelsList = new List<Parcel>();
            foreach (Parcel item in DataSource.parcels)
            {
                if (item.Droneld == 0 && item.Scheduled == new DateTime(01, 01, 0001))
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
                Drone myDrone = getDrone(droneToUpdate.Id);
                Parcel myParcel = getParcel(parcelToUpdate.Id);

                DataSource.drones.Remove(droneToUpdate);
                DataSource.parcels.Remove(parcelToUpdate);

                myParcel.Droneld = myDrone.Id;
                myParcel.Scheduled = DateTime.Now;

                DataSource.parcels.Add(myParcel);
            }
            catch (DoesntExistentObjectException ex)
            {

                throw new DoesntExistentObjectException("",ex);
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
                throw new DoesntExistentObjectException("", ex);
            }
            
                  
        }

        public void delivery_arrive_toCustomer(Drone droneToUpdate, Parcel parcelToUpdate)//The delivery arrived to the customer
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
                throw new DoesntExistentObjectException("", ex);
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

                    DroneCharge droneChargeToAdd = new DroneCharge() { Droneld = myDrone.Id, Stationld = myStation.Id };
                    DataSource.dronesCharge.Add(droneChargeToAdd);
                    return;
                }

                throw new chargingException("The station doesn't have available charging slot");
            }
            catch (DoesntExistentObjectException ex)
            {

                throw new DoesntExistentObjectException("", ex);
            }
                    
        }

        public void freeDroneCharge(Drone droneToUpdate)//Drone release from charging
        {
            try
            {
                DroneCharge droneChargeToDel = DataSource.dronesCharge.Find(x => x.Droneld == droneToUpdate.Id);

                Station myStation = getStation(droneChargeToDel.Stationld);

                if (droneChargeToDel.Droneld != 0 && droneChargeToDel.Stationld != 0)
                {
                    delFromChargingDrone(droneChargeToDel);
                    
                    return;
                }

                throw new chargingException("The drone doesn't charging in this station");
            }
            catch (DoesntExistentObjectException ex)
            {

                throw new DoesntExistentObjectException("", ex);
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

        public void getParcel(Parcel parcelItem)
        {
            throw new NotImplementedException();
        }
        #endregion

    }

}

