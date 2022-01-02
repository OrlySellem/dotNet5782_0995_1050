using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace Dal
{
    sealed class DalXml :IDal
    {
        #region singelton

        static readonly IDal instance = new DalXml();
        public static IDal Instance { get => instance; }
        DalXml() { }

        #endregion singelton

        public void addStaion(Station stationToAdd)
        {
            throw new NotImplementedException();
        }

        public void addDrone(Drone DroneToAdd)
        {
            throw new NotImplementedException();
        }

        public void addCustomer(Customer CustomerToAdd)
        {
            throw new NotImplementedException();
        }

        public void addParcel(Parcel ParcelToAdd)
        {
            throw new NotImplementedException();
        }

        public Parcel getParcel(int id)
        {
            throw new NotImplementedException();
        }

        public Drone getDrone(int id)
        {
            throw new NotImplementedException();
        }

        public Station getStation(int id)
        {
            throw new NotImplementedException();
        }

        public Customer getCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public DroneCharge getDroneCharge(int id)
        {
            throw new NotImplementedException();
        }

        public void delFromDrones(Drone droneToDel)
        {
            throw new NotImplementedException();
        }

        public void delFromStations(Station stationToDel, bool free)
        {
            throw new NotImplementedException();
        }

        public void delFromParcels(Parcel parcelToDel)
        {
            throw new NotImplementedException();
        }

        public void delFromCustomers(Customer customerToDel)
        {
            throw new NotImplementedException();
        }

        public void delFromChargingDrone(DroneCharge droneCharge)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parcel> getParcels(Predicate<Parcel> prdicat = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DroneCharge> getDronesCharge(Predicate<DroneCharge> prdicat = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Drone> getDrones(Predicate<Drone> prdicat = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> getStations(Predicate<Station> prdicat = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> getCustomers(Predicate<Customer> prdicat = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parcel> print_unconnected_parcels_to_Drone()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> print_stations_with_freeDroneCharge()
        {
            throw new NotImplementedException();
        }

        public void reduceChargeSlots(ref Station s)
        {
            throw new NotImplementedException();
        }

        public void plusChargeSlots(ref Station s)
        {
            throw new NotImplementedException();
        }

        public void assign_drone_parcel(Drone droneToUpdate, Parcel parcelToUpdate)
        {
            throw new NotImplementedException();
        }

        public void drone_pick_parcel(Drone droneToUpdate, Parcel parcelToUpdate)
        {
            throw new NotImplementedException();
        }

        public void delivery_arrive_toCustomer(Parcel parcelToUpdate)
        {
            throw new NotImplementedException();
        }

        public void chargingDrone(Drone droneToUpdate, Station stationToUpdate)
        {
            throw new NotImplementedException();
        }

        public void freeDroneCharge(Drone droneToUpdate)
        {
            throw new NotImplementedException();
        }

        public double[] R_power_consumption_Drone()
        {
            throw new NotImplementedException();
        }
    }


}