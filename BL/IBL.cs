using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;

namespace BlApi
{
    public interface IBL
    {
        public void openSimulator(int idDrone, Action update, Func<bool> checkStop);

        #region ADD
        public void addStation(Station StationToAdd);

        public void addDrone(Drone DroneToAdd, int idStation);// add drone

        public void addCustomer(Customer CustomerToAdd);// add customer

        public void addParcel(Parcel ParcelToAdd);//add new base percel
        #endregion

        #region update
        public void  updateStation(int id, int name_int, int chargeSlots);

        public void updateCustomer(int id, string newName, string newPhone);

        public void chargingDrone(int droneId);

        public void updateModelDrone(int idDrone, string newModel);

        public void freeDroneFromCharging(int idDrone, DateTime newtime);

        public void assignDroneToParcel(int idDrone);

        public void dronePickParcel(int idDrone);

        public void deliveryArivveToCustomer(int idDrone);

        #endregion

        #region gat single

        public Station getStation(int id);

        public Parcel getParcel(int id);

        public DroneToList getDrone(int id);

        public Customer getCustomer(int id);

        #endregion

        #region getAll
        public IEnumerable<StationToList> getAllStations(Predicate<StationToList> predicate = null);
        public IEnumerable<DroneToList> GetDrones(Predicate<DroneToList> predicate = null);
        public IEnumerable<CustomerToList> getAllCustomers(Predicate<CustomerToList> predicate = null);
        public IEnumerable<ParcelToList> getAllParcels(Predicate<ParcelToList> predicate = null);
        public IEnumerable<ParcelToList> ParcelDoesntAssignToDrone();
        public IEnumerable<StationToList> display_station_with_freeChargingStations();

        #endregion


        #region DELETE

        public void deleteFromDrones(int IDdroneToDel);

        public void deleteFromStations(int IDstationToDel);

        public void deleteFromParcels(int IDparcelToDel);

        public void deleteFromCustomers(int IDcustomerToDel);

      
        #endregion DELETE
        // 




    }
}
