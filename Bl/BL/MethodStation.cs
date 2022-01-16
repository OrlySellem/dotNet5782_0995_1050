using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using BO;
using BlApi;


namespace BL
{
    partial class BL : IBL
    {
        #region CRUD

        //Create
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addStation(Station stationToAdd)
        {
            lock (dal)
            {
                try
                {
                    DO.Station dalStation = new DO.Station()
                    {
                        Id = stationToAdd.Id,
                        Name = stationToAdd.Name,
                        Lattitude = stationToAdd.Address.Lattitude,
                        Longitude = stationToAdd.Address.Longitude,
                        ChargeSlots = stationToAdd.ChargeSlots
                    };

                    dal.addStaion(dalStation);
                }
                catch (DO.AlreadyExistException ex)
                {
                    throw new AlreadyExistException(ex.Message);
                }
            }

          
        }

        // Read
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station getStation(int id)
        {
            lock (dal)
            {
                try
                {
                    DO.Station s = dal.getStation(id);

                    Location address = new Location()
                    {
                        Longitude = s.Longitude,
                        Lattitude = s.Lattitude
                    };

                    List<DO.DroneCharge> chargingDronesDAL = (List<DO.DroneCharge>)dal.getDronesCharge().ToList();

                    List<ChargingDrone> chargingDronesBL = new List<ChargingDrone>();

                    foreach (var item in chargingDronesDAL)
                    {
                        if (item.Stationld == s.Id)
                        {

                            var itemBL = new ChargingDrone()
                            {
                                Id = item.Droneld,
                                Battery = getDrone(item.Droneld).Battery
                            };

                            chargingDronesBL.Add(itemBL);
                        }

                    }

                    return new BO.Station
                    {
                        Id = s.Id,

                        Name = s.Name,

                        Address = address,

                        ChargeSlots = s.ChargeSlots,

                        Charging_drones = chargingDronesBL
                    };
                }
                catch (DO.DoesntExistentObjectException ex)
                {

                    throw new DoesntExistentObjectException(ex.Message);
                }

            }

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToList> getAllStations(Predicate<StationToList> predicate = null)
        {
            lock (dal)
            {
                IEnumerable<DO.Station> StationsList_dal = dal.getStations().ToList();
                List<StationToList> StationList_bl = new List<StationToList>();
                IEnumerable<DO.DroneCharge> droneChargeList = dal.getDronesCharge().ToList();


                foreach (var stationItem in StationsList_dal)
                {
                    int ChargeSlotsFull_conster = 0;

                    foreach (var droneChargeItem in droneChargeList)
                    {
                        if (droneChargeItem.Stationld == stationItem.Id)
                            ChargeSlotsFull_conster++;
                    }

                    StationToList addStation = new StationToList()
                    {
                        Id = stationItem.Id,
                        Name = stationItem.Name,
                        ChargeSlotsFree = stationItem.ChargeSlots,
                        ChargeSlotsFull = ChargeSlotsFull_conster

                    };
                    StationList_bl.Add(addStation);
                }

                return StationList_bl;
            }
           
        }

        //Update
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateStation(int idStation, string name, int chargeSlots)
        {
            lock (dal)
            {
                try
                {
                    var updateStation = dal.getStation(idStation);
                    List<DO.DroneCharge> chargingDrones = dal.getDronesCharge().ToList();

                    dal.delFromStations(updateStation, false);

                    if (name != "")
                    {
                        updateStation.Name = name;
                    }

                    if (chargeSlots > 0)
                    {
                        int ChargeSlotsFull = chargingDrones.Count(x => x.Stationld == idStation);

                        updateStation.ChargeSlots += chargeSlots;
                    }

                    dal.addStaion(updateStation);
                }
                catch (DO.DoesntExistentObjectException ex)
                {
                    throw new DoesntExistentObjectException(ex.Message);
                }
                catch (DO.AlreadyExistException ex)
                {
                    throw new AlreadyExistException(ex.Message);
                }
            }
           
        }

        //Delete
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deleteFromStations(int IDstationToDel)
        {
            lock (dal)
            {
                DO.Station station = dal.getStation(IDstationToDel);
                dal.delFromStations(station, true);
            }
            
        }

        #endregion CRUD

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToList> display_station_with_freeChargingStations()
        {
            lock (dal)
            {
                var stationList_dal = dal.stations_with_freeDroneCharge();
                List<StationToList> StationList_bl = new List<StationToList>();
                IEnumerable<DO.DroneCharge> droneChargeList = dal.getDronesCharge().ToList();
                
                foreach (var stationItem in stationList_dal)
                {
                    int ChargeSlotsFull_conster = 0;
                    foreach (var droneChargeItem in droneChargeList)
                    {
                        if (droneChargeItem.Stationld == stationItem.Id)
                            ChargeSlotsFull_conster++;
                    }

                    StationToList addStation = new StationToList()
                    {
                        Id = stationItem.Id,
                        Name = stationItem.Name,
                        ChargeSlotsFree = stationItem.ChargeSlots,
                        ChargeSlotsFull = ChargeSlotsFull_conster
                    };

                    StationList_bl.Add(addStation);
                }

                return StationList_bl;
            }
         
        }


    }

}








