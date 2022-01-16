using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi
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

        public DroneCharge getDroneCharge(int id);
        #endregion

        #region REMOVE
        public void delFromDrones(Drone droneToDel);

        public void delFromStations(Station stationToDel, bool free);

        public void delFromParcels(Parcel parcelToDel);

        public void delFromCustomers(Customer customerToDel);

        public void delFromChargingDrone(DroneCharge droneCharge);
        #endregion REMOVE

        #region GET_LIST
        public IEnumerable<Parcel> getParcels(Predicate<Parcel> prdicat = null);

        public IEnumerable<DroneCharge> getDronesCharge(Predicate<DroneCharge> prdicat = null);

        public IEnumerable<Drone> getDrones(Predicate<Drone> prdicat = null);

        public IEnumerable<Station> getStations(Predicate<Station> prdicat = null);//return list of stations

        public IEnumerable<Customer> getCustomers(Predicate<Customer> prdicat = null);

        public IEnumerable<Parcel> print_unconnected_parcels_to_Drone();

        public IEnumerable<Station> stations_with_freeDroneCharge();

        #endregion

        #region updateChargeSlots
        public void reduceChargeSlots(ref Station s);

        public void plusChargeSlots(ref Station s);
        #endregion

        #region update
        public void assign_drone_parcel(Drone droneToUpdate, Parcel parcelToUpdate);//assign parcel to drone

        public void drone_pick_parcel(Drone droneToUpdate, Parcel parcelToUpdate);//pick up parcel by drone

        public void delivery_arrive_toCustomer(Parcel parcelToUpdate, DateTime Delivered);//The delivery arrived to the customer

        public void chargingDrone(Drone droneToUpdate, Station stationToUpdate);//Inserts a drone to charg

        public void freeDroneCharge(Drone droneToUpdate);//Drone release from charging
        #endregion

        #region power
        public double[] R_power_consumption_Drone();
        #endregion
    }
}
