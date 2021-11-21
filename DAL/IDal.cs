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

        public void addStaion();//add new base station

        public void addDrone();// add drone
  
        public void addCustomer();// add customer

        public void addParcel();//add new base percel
       
        public int findIndexParcel(int id);//Finds the requested parcel from the arr

        public int findIndexDrone(int id);//Finds the requested drone from the arr
 
        public int findIndexStation(int id);//Finds the requested station from the arr

        public int findIndexCustomer(int id);//loop to find the customer acordding to ID 

        public int checkIndexParcel();//Receives a parcel from the user and make sure that it exists

        public int checkIndexDrone();//Receives a drone from the user and make sure that it exists
 
        public int checkIndexStation();//Receives a station from the user and make sure that it exists
 
        public void assign_parcel_drone();//assign parcel to drone

        public void drone_pick_parcel();//pick up parcel by drone

        public void delivery_arrive_toCustomer();//The delivery arrived to the customer

        public void chargingDrone();//Inserts a drone to charg

        public void freeDroneCharge();//Drone release from charging

        public void printStation(int id);//print  the requested Station

        public void printDrone(int id);//print the requested Drone

        public void printCustomer(int id);//print the requested Customer

        public void printParcel(int id);//print the requested parcel

        public void printAllStations();

        public void printAllDrones();

        public void printAllCustomers();

        public void printAllParcels();

        public void print_unconnected_parcels_to_Drone();

        public void print_stations_with_freeDroneCharge();

        public List <double> R_power_consumption_Drone();
    }
}
