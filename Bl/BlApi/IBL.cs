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
    {/// <summary>
    /// 
    /// </summary>
    /// <param name="idDrone"></param>
    /// <param name="update"></param>
    /// <param name="checkStop"></param>
        public void openSimulator(int idDrone, Action update, Func<bool> checkStop);

        #region ADD
        /// <summary>
        /// Function for adding a base station to system
        /// </summary>
        /// <param name="StationToAdd"></param>
        public void addStation(Station StationToAdd);

        /// <summary>
        /// Function for adding a drone to system
        /// </summary>
        /// <param name="DroneToAdd"></param>
        /// <param name="idStation"></param>
        public void addDrone(Drone DroneToAdd, int idStation);// add drone

        /// <summary>
        /// Function for adding a customer to system
        /// </summary>
        /// <param name="CustomerToAdd"></param>
        public void addCustomer(Customer CustomerToAdd);// add customer

        /// <summary>
        /// Function for adding parcel to system
        /// </summary>
        /// <param name="ParcelToAdd"></param>
        public void addParcel(Parcel ParcelToAdd);//add new base percel
        #endregion

        #region update
        /// <summary>
        /// Function for updating 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name_int"></param>
        /// <param name="chargeSlots"></param>
        public void  updateStation(int id, string name, int chargeSlots);

        public void updateCustomer(int id, string newName, string newPhone);

        public void chargingDrone(int droneId);

        public void updateModelDrone(int idDrone, string newModel);

        public void freeDroneFromCharging(int idDrone, DateTime newtime);

        public void assignDroneToParcel(int idDrone, DateTime newtime);

        public void dronePickParcel(int idDrone, DateTime newtime);

        public void deliveryArivveToCustomer(int idDrone, DateTime newtime);

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
