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

        public Station getStation (int id)
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

        public IEnumerable <StationToList> getAllStations()
        {

            
        }

        public void updateStation(int idStation, int name_int, int chargeSlots)
        {
            try
            {
                var updateStation = dal.getStation(idStation);

                var chargeSlotsDal = dal.getAllDroneCharge();

                dal.delFromStations(updateStation);

                if (name_int != 0)
                {
                    updateStation.Name = name_int;
                }

                if (chargeSlots != 0)
                {
                    int cs = chargeSlotsDal.Count(x => x.Stationld == idStation);

                    updateStation.ChargeSlots = chargeSlots - cs;
                }

                dal.addStaion(updateStation);
            }
           catch (IDAL.DO.DoesntExistException ex)
            {
                throw new GetDetailsProblemException("The station doesn't exist in the system");
            }
           catch (IDAL.DO.AlreadyExistException ex)
            {
                throw new AddingProblemException("The station already exist in the system");
            }
        }

    }
}
