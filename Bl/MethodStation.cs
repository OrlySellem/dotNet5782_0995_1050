using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;


namespace IBL
{
    public partial class BL : IBL
    {
        public void addStation(Station stationToAdd)
        {
            try
            {
                IDAL.DO.Station dalStation = new IDAL.DO.Station()
                {
                    Id = stationToAdd.Id,
                    Name = stationToAdd.Name,
                    Lattitude = stationToAdd.Address.Lattitude,
                    Longitude = stationToAdd.Address.Longitude,
                    ChargeSlots = stationToAdd.ChargeSlots
                };

                dal.addStaion(dalStation);
            }
            catch (IDAL.DO.DoesntExistentObjectException ex)
            {
                throw new DoesntExistentObjectException(ex.Message);
            }

        }

        public Station getStation(int id)
        {
            try
            {
                IDAL.DO.Station s = dal.getStation(id);

                Location address = new Location()
                {
                    Longitude = s.Longitude,
                    Lattitude = s.Lattitude
                };

                List<IDAL.DO.DroneCharge> chargingDronesDAL = (List<IDAL.DO.DroneCharge>)dal.getDronesCharge().ToList();

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

                return new BO.Station//have to add chargingDrone list!! - לעשות
                {
                    Id = s.Id,

                    Name = s.Name,

                    Address = address,

                    ChargeSlots = s.ChargeSlots,

                    Charging_drones = chargingDronesBL
                };
            }
            catch (IDAL.DO.DoesntExistentObjectException ex)
            {

                throw new DoesntExistentObjectException(ex.Message);
            }

        }

        public IEnumerable<StationToList> getAllStations(Predicate<StationToList> predicate = null)
        {
            IEnumerable<IDAL.DO.Station> StationsList_dal = dal.getStations().ToList();
            List<StationToList> StationList_bl = new List<StationToList>();
            IEnumerable<IDAL.DO.DroneCharge> droneChargeList = dal.getDronesCharge().ToList();


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

        public void updateStation(int idStation, int name_int, int chargeSlots)
        {
            try
            {
                var updateStation = dal.getStation(idStation);
                List<IDAL.DO.DroneCharge> chargingDrones = (List<IDAL.DO.DroneCharge>)dal.getDronesCharge().ToList();
     
                dal.delFromStations(updateStation, false);

                var chargeSlotsDal = dal.getDronesCharge().ToList();

                if (name_int != 0)
                {
                    updateStation.Name = name_int;
                }

                if (chargeSlots != 0)
                {
                    int notAvailable_chargeSlots = chargeSlotsDal.Count(x => x.Stationld == idStation);

                    updateStation.ChargeSlots = chargeSlots - notAvailable_chargeSlots;
                }

                dal.addStaion(updateStation);
            }
            catch (IDAL.DO.DoesntExistentObjectException ex)
            {
                throw new DoesntExistentObjectException(ex.Message);
            }
            catch (IDAL.DO.AlreadyExistException ex)
            {
                throw new AlreadyExistException(ex.Message);
            }
        }




        public IEnumerable<StationToList> display_station_with_freeChargingStations()
        {
            var stationList_dal = dal.print_stations_with_freeDroneCharge();
            List<StationToList> StationList_bl = new List<StationToList>();
            IEnumerable<IDAL.DO.DroneCharge> droneChargeList = dal.getDronesCharge().ToList();

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








