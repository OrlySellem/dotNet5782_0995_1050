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

        string customerPath = @"CustomerXml.xml";//XElement
        string dronePath = @"DroneXml.xml";  //XElement
        string droneChargePath = @"DroneChargeXml.xml";//XElement
        string parcelPath = @"ParcelXml.xml";//XMLSerializer
        string stationPath = @"Station.xml";//XMLSerializer


        #region ADD
        public void addStaion(Station stationToAdd)
        {


            var listStation = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            if (listStation.Exists(x => x.Id == stationToAdd.Id))
                throw new AlreadyExistException("station");

            listStation.Add(stationToAdd);
            XMLTools.SaveListToXMLSerializer(listStation, stationPath);

            // throw new NotImplementedException();
        }

        public void addDrone(Drone DroneToAdd)
        {

            var listDrone = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
            if (listDrone.Exists(x => x.Id == DroneToAdd.Id))
                throw new AlreadyExistException("drone");

            listDrone.Add(DroneToAdd);
            XMLTools.SaveListToXMLSerializer(listDrone, dronePath);

           // throw new NotImplementedException();
        }

        public void addCustomer(Customer CustomerToAdd)
        {
            var listCustomer = XMLTools.LoadListFromXMLSerializer<Customer>(customerPath);
            if (listCustomer.Exists(x => x.Id == CustomerToAdd.Id))
                throw new AlreadyExistException("customer");

            listCustomer.Add(CustomerToAdd);
            XMLTools.SaveListToXMLSerializer(listCustomer, customerPath);

            // throw new NotImplementedException();
        }

        public void addParcel(Parcel ParcelToAdd)
        {
            var listParcel = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            if (listParcel.Exists(x => x.Id == ParcelToAdd.Id))
                throw new AlreadyExistException("parcel");

            listParcel.Add(ParcelToAdd);
            XMLTools.SaveListToXMLSerializer(listParcel, parcelPath);

            // throw new NotImplementedException();
        }

        #endregion ADD

        #region GET 
        public Parcel getParcel(int id)
        {
            var listParcel = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            Parcel parcel = (from findparcel in listParcel
                             where findparcel.Id == id
                             select findparcel).FirstOrDefault();
            if (parcel.Equals(default(Parcel)))
                throw new DoesntExistentObjectException("parcel");
            return parcel;

            //  throw new NotImplementedException();
        }

        public Drone getDrone(int id)
        {
            var listDrone = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
            Drone drone = (from findDrone in listDrone
                             where findDrone.Id == id
                             select findDrone).FirstOrDefault();
            if (drone.Equals(default(Parcel)))
                throw new DoesntExistentObjectException("drone");
            return drone;

            //  throw new NotImplementedException();
        }

        public Station getStation(int id)
        {
            var listStation = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            Station station = (from findStation in listStation
                             where findStation.Id == id
                             select findStation).FirstOrDefault();
            if (station.Equals(default(Parcel)))
                throw new DoesntExistentObjectException("station");
            return station;

            //  throw new NotImplementedException();
        }

        public Customer getCustomer(int id)
        {
            var listCustomer = XMLTools.LoadListFromXMLSerializer<Customer>(customerPath);
            Customer customer = (from findCustomer in listCustomer
                             where findCustomer.Id == id
                             select findCustomer).FirstOrDefault();
            if (customer.Equals(default(Parcel)))
                throw new DoesntExistentObjectException("customer");
            return customer;

            //  throw new NotImplementedException();
        }

        public DroneCharge getDroneCharge(int id)
        {
            var listDroneCharge = XMLTools.LoadListFromXMLSerializer<DroneCharge>(parcelPath);
            DroneCharge droneCharge = (from findDroneCharge in listDroneCharge
                             where findDroneCharge.Droneld == id
                             select findDroneCharge).FirstOrDefault();
            if (droneCharge.Equals(default(DroneCharge)))
                throw new chargingException("the drone is not charging");
            return droneCharge;

            //  throw new NotImplementedException();
        }

        #endregion GET 
        public void delFromDrones(Drone droneToDel)
        {
            var listParcel = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            Parcel parcelToUpdate = listParcel.Find(x => x.Droneld == droneToDel.Id);
            if (parcelToUpdate.Id != 0)
            {
                if (parcelToUpdate.Delivered == null)
                {
                    delFromParcels(parcelToUpdate);
                    parcelToUpdate.Scheduled = null;
                    parcelToUpdate.PickedUp = null;
                    parcelToUpdate.Droneld = 0;
                    addParcel(parcelToUpdate);
                }
            }
            var listDrone = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
            listDrone.Remove(droneToDel);
            XMLTools.SaveListToXMLSerializer(listDrone, dronePath);

            //throw new NotImplementedException();
        }

        public void delFromStations(Station stationToDel, bool freeDrone)
        {
            if (freeDrone)
            {
                var listDronesCharge = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneChargePath);
                foreach (var dronesCharge in listDronesCharge)
                {
                    if (dronesCharge.Stationld == stationToDel.Id)
                    {
                        Drone droneToUpdate = getDrone(dronesCharge.Droneld);
                        freeDroneCharge(droneToUpdate);
                    }
                }
            }
            var listStation = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            listStation.Remove(stationToDel);
            XMLTools.SaveListToXMLSerializer(listStation, stationPath);

           

            // throw new NotImplementedException();
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