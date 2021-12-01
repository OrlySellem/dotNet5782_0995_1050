using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IDAL.DO;

namespace IDAL
{
    public interface IDal
    {
        internal static Random rand = new Random();

        #region ADD
        public void addStaion(Station stationToAdd);//add new base station

        public void addDrone(Drone DroneToAdd);// add drone
  
        public void addCustomer(Customer CustomerToAdd);// add customer

        public void addParcel(Parcel ParcelToAdd);//add new base percel

        #endregion

        #region GET
        public Parcel getParcel(int id);//Finds the requested parcel from the arr

        public Drone getDrone(int id);//Finds the requested drone from the arr

        public Station getStation(int id);//Finds the requested station from the arr

        public Customer getCustomer(int id);//loop to find the customer acordding to ID 

        #endregion

        #region updateChargeSlots
        public void reduceChargeSlots(ref Station s);

        public void plusChargeSlots(ref Station s);
        #endregion

        #region update
        public void assign_drone_parcel(Drone droneToUpdate, Parcel parcelToUpdate);//assign parcel to drone

        public void drone_pick_parcel(Drone droneToUpdate, Parcel parcelToUpdate);//pick up parcel by drone

        public void delivery_arrive_toCustomer(Drone droneToUpdate, Parcel parcelToUpdate);//The delivery arrived to the customer

        public void chargingDrone(Drone droneToUpdate, Station stationToUpdate);//Inserts a drone to charg

        public void freeDroneCharge(Drone droneToUpdate, Station stationToUpdate);//Drone release from charging
        #endregion

        #region power
        public double [] R_power_consumption_Drone();
        #endregion
    }
}
