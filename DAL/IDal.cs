using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;

namespace IDAL.DO
{
    public interface IDal
    {
        internal static Random rand = new Random();

        #region add_Dal
        public void addStaion(Station stationToAdd);//add new base station

        public void addDrone(Drone DroneToAdd);// add drone
  
        public void addCustomer(Customer CustomerToAdd);// add customer

        public void addParcel(Parcel ParcelToAdd);//add new base percel

        #endregion
        public int findIndexParcel(int id);//Finds the requested parcel from the arr

        public int findIndexDrone(int id);//Finds the requested drone from the arr
 
        public int findIndexStation(int id);//Finds the requested station from the arr

        public int findIndexCustomer(int id);//loop to find the customer acordding to ID 

        public void assign_parcel_drone();//assign parcel to drone

        public void drone_pick_parcel();//pick up parcel by drone

        public void delivery_arrive_toCustomer();//The delivery arrived to the customer

        public void chargingDrone();//Inserts a drone to charg

        public void freeDroneCharge();//Drone release from charging

        public double [] R_power_consumption_Drone();
    }
}
