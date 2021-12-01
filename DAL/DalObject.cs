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
        public void addStaion(Station stationToAdd) //דוגמא בויה לסידור +ה find
        {
            Station temp = DataSource.stations.Find(x => x.Id == stationToAdd.Id);
            if (temp.Id!=0)
                throw new stationException("already exist");

            DataSource.stations.Add(stationToAdd);
        }

        public void addDrone(Drone droneToAdd)
        {
            Drone temp = DataSource.drones.Find(x => x.Id == droneToAdd.Id);
            if (temp.Id != 0)
                throw new droneException("already exist");

            DataSource.drones.Add(droneToAdd);
        }

        public void addCustomer(Customer CustomerToAdd)
        {
            Customer temp = DataSource.customers.Find(x => x.Id == CustomerToAdd.Id);
            if (temp.Id != 0)
                throw new customerException("already exist");

            DataSource.customers.Add(CustomerToAdd);
        }

        public void addParcel(Parcel ParcelToAdd)
        {
            Parcel temp = DataSource.parcels.Find(x => x.Id == ParcelToAdd.Id);
            if (temp.Id != 0)
                throw new parcelException("already exist");
            temp.Id = DataSource.Config.idParcel++;
            DataSource.parcels.Add(ParcelToAdd);
        }
        #endregion

        #region GET 
        public Parcel getParcel(int id)//Finds the requested parcel from the arr
        {
           Parcel parcel = DataSource.parcels.Find(p => p.Id == id);
            if (parcel.Id == 0)
                throw new stationException("isn't exist");
            return parcel;
        }

        public Drone getDrone(int id)//Finds the requested drone from the arr
        {
            Drone drone = DataSource.drones.Find(d => d.Id == id);
            if (drone.Id == 0)
                throw new stationException("isn't exist");
            return drone;
        }

        //מעודכן להראות לאורלי
        public Station getStation(int id)//Finds the requested station from the arr
        {
            Station station = DataSource.stations.Find(s => s.Id == id);
            if (station.Id == 0)
                throw new stationException("isn't exist");
            return station;
        }

        public Customer getCustomer(int id)//loop to find the customer acordding to ID 
        {
            Customer customer = DataSource.customers.Find(c => c.Id == id);
            if (customer.Id == 0)
                throw new stationException("isn't exist");
            return customer;
        }
        #endregion

        #region updateChargeSlots
        public void reduceChargeSlots(ref Station s)
        {
            s.ChargeSlots--;//Reduce the number of claim positions
        }

        public void plusChargeSlots (ref Station s)
        {
            s.ChargeSlots++;
        }
        #endregion

        #region update
        public void assign_drone_parcel (Drone droneToUpdate, Parcel parcelToUpdate)//assign drone to parcel
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
                return;
            }

           throw new stationException("isn't available");

            DroneCharge droneChargeToAdd = new DroneCharge() { Droneld = myDrone.Id, Stationld = myStation.Id };
            DataSource.dronesCharge.Add(droneChargeToAdd);          
        }
    
        public void freeDroneCharge(Drone droneToUpdate, Station stationToUpdate)//Drone release from charging
        {
            Drone myDrone = getDrone(droneToUpdate.Id);
            Station myStation = getStation(stationToUpdate.Id);
        
            DroneCharge toDelete = DataSource.dronesCharge.Find(x=> x.Droneld == myDrone.Id && x.Stationld == myStation.Id);
            
            if(toDelete.Droneld!=0 && toDelete.Stationld!=0)
            {
                DataSource.dronesCharge.Remove(toDelete);

                DataSource.stations.Remove(stationToUpdate);
                plusChargeSlots(ref myStation);
                DataSource.stations.Add(myStation);
                return;
            }

            throw new DroneChargeException("The drone doesn't charge in this station");
        }
        #endregion

        #region power
        public double [] R_power_consumption_Drone()
        {
            double [] power = new double [5];
            power[0]=DataSource.Config.available;
            power[1]=DataSource.Config.Lightweight;
            power[2]=DataSource.Config.MediumWeight;
            power[3]=DataSource.Config.Heavyweight;
            power[4]=DataSource.Config.Drone_charging_speed;

            return power;
        }
        #endregion

    }

}

