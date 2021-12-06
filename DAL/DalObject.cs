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
                throw new AlreadyExistException("The station already exist");

            DataSource.stations.Add(stationToAdd);
        }

        public void addDrone(Drone droneToAdd)
        {
            Drone temp = DataSource.drones.Find(x => x.Id == droneToAdd.Id);
            if (temp.Id != 0)
                throw new AlreadyExistException("The drone already exist");

            DataSource.drones.Add(droneToAdd);
        }

        public void addCustomer(Customer CustomerToAdd)
        {
            Customer temp = DataSource.customers.Find(x => x.Id == CustomerToAdd.Id);
            if (temp.Id != 0)
                throw new AlreadyExistException("The customer already exist");

            DataSource.customers.Add(CustomerToAdd);
        }

        public void addParcel(Parcel ParcelToAdd)
        {
            Parcel temp = DataSource.parcels.Find(x => x.Id == ParcelToAdd.Id);
            if (temp.Id != 0)
                throw new AlreadyExistException("The parcel already exist");
            temp.Id = DataSource.Config.idParcel++;
            DataSource.parcels.Add(ParcelToAdd);
        }
        #endregion

        #region REMOVE
        public void delFromDrones(Drone droneToDel)
        {
            DataSource.drones.Remove(droneToDel);
        }

        public void delFromStations(Station stationToDel)
        {
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

        #endregion

        #region GET 
        public Parcel getParcel(int id)//Finds the requested parcel from the arr
        {
            Parcel parcel = DataSource.parcels.Find(p => p.Id == id);
            if (parcel.Id == 0)
                throw new DoesntExistException("The parcel doesn't exist in the system");
            return parcel;
        }

        public Drone getDrone(int id)//Finds the requested drone from the arr
        {
            Drone drone = DataSource.drones.Find(d => d.Id == id);
            if (drone.Id == 0)
                throw new DoesntExistException("The drone doesn't exist in the system");
            return drone;
        }

        public Station getStation(int id)//Finds the requested station from the arr
        {
            Station station = DataSource.stations.Find(s => s.Id == id);
            if (station.Id == 0)
                throw new DoesntExistException("The station doesn't exist in the system");
            return station;
        }

        public Customer getCustomer(int id)//loop to find the customer acordding to ID 
        {
            Customer customer = DataSource.customers.Find(c => c.Id == id);
            if (customer.Id == 0)
                throw new DoesntExistException("The customer doesn't exist in the system");
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

        #endregion GET_LIST

        #region updateChargeSlots
        public void reduceChargeSlots(ref Station s)
        {
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
            Drone myDrone = getDrone(droneToUpdate.Id);
            Parcel myParcel = getParcel(parcelToUpdate.Id);

            DataSource.drones.Remove(droneToUpdate);
            DataSource.parcels.Remove(parcelToUpdate);

            myParcel.Droneld = myDrone.Id;
            myParcel.Scheduled = DateTime.Now;

            DataSource.parcels.Add(myParcel);
        }

        public void drone_pick_parcel(Drone droneToUpdate, Parcel parcelToUpdate)//pick up parcel by drone
        {
            Drone myDrone = getDrone(droneToUpdate.Id);
            Parcel myParcel = getParcel(parcelToUpdate.Id);

            DataSource.drones.Remove(droneToUpdate);
            DataSource.parcels.Remove(parcelToUpdate);

            myParcel.PickedUp = DateTime.Now;

            DataSource.drones.Add(myDrone);
            DataSource.parcels.Add(myParcel);

        }

        public void delivery_arrive_toCustomer(Drone droneToUpdate, Parcel parcelToUpdate)//The delivery arrived to the customer
        {
            getDrone(droneToUpdate.Id);
            Parcel myParcel = getParcel(parcelToUpdate.Id);

            DataSource.parcels.Remove(parcelToUpdate);

            myParcel.Delivered = DateTime.Now;

            DataSource.parcels.Add(myParcel);
        }

        public void chargingDrone(Drone droneToUpdate, Station stationToUpdate)//Inserts a drone to charg
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

        public void freeDroneCharge(Drone droneToUpdate, Station stationToUpdate)//Drone release from charging
        {
            Drone myDrone = getDrone(droneToUpdate.Id);
            Station myStation = getStation(stationToUpdate.Id);

            DroneCharge toDelete = DataSource.dronesCharge.Find(x => x.Droneld == myDrone.Id && x.Stationld == myStation.Id);

            if (toDelete.Droneld != 0 && toDelete.Stationld != 0)
            {
                DataSource.dronesCharge.Remove(toDelete);

                DataSource.stations.Remove(stationToUpdate);
                plusChargeSlots(ref myStation);
                DataSource.stations.Add(myStation);
                return;
            }

            throw new chargingException("The drone doesn't charge in this station");
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

