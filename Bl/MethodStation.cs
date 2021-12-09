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
            catch (IDAL.DO.DoesntExistException ex)
            {
                throw new AddingProblemException("The station already exist", ex);
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

                return new BO.Station//have to add chargingDrone list!! - לעשות
                {
                    Id = s.Id,

                    Name = s.Name,

                    Address = address,

                    ChargeSlots = s.ChargeSlots,
                };
            }
            catch (IDAL.DO.DoesntExistException ex)
            {

                throw new GetDetailsProblemException("The station doesn't exist in the system", ex);
            }

        }

        public IEnumerable<StationToList> getAllStations()
        {
            IEnumerable<IDAL.DO.Station> StationsList_dal = dal.getAllStation();
            List<StationToList> StationList_bl = new List<StationToList>();
            IEnumerable<IDAL.DO.DroneCharge> droneChargeList = dal.getAllDroneCharge();


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
                dal.delFromStations(updateStation);

                var chargeSlotsDal = dal.getAllDroneCharge();

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
            catch (IDAL.DO.DoesntExistException ex)
            {
                throw new GetDetailsProblemException("The station doesn't exist in the system", ex);
            }
            catch (IDAL.DO.AlreadyExistException ex)
            {
                throw new AddingProblemException("The station already exist in the system", ex);
            }
        }




        public IEnumerable<StationToList> display_station_with_freeChargingStations()
        {
            var stationList_dal = dal.print_stations_with_freeDroneCharge();
            List<StationToList> StationList_bl = new List<StationToList>();
            IEnumerable<IDAL.DO.DroneCharge> droneChargeList = dal.getAllDroneCharge();

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




