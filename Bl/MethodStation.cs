using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;


namespace IBL
{
    public partial class BL :IBL
    {
        public void addStation(Station stationToAdd)
        {       
            IDAL.DO.Station dalStation = new IDAL.DO.Station()
            {
                Id = stationToAdd.Id,
                Name= stationToAdd.Name,
                Lattitude= stationToAdd.Address.Lattitude,
                Longitude= stationToAdd.Address.Longitude,
                ChargeSlots= stationToAdd.ChargeSlots
            };

            dal.addStaion(dalStation);
        }

        public Station getStation (int id)
        {
            IDAL.DO.Station s = dal.getStation(id);

            Location address = new Location()
            {
                Longitude=s.Longitude,
                Lattitude=s.Lattitude
            };

            return new BO.Station//have to add chargingDrone list!! - לעשות
            {
                Id = s.Id,

                Name = s.Name,

                Address = address,

                ChargeSlots = s.ChargeSlots,
            };  
        }

        public IEnumerable <StationToList> getAllStations()
        {

            
        }

        public void updateStation(int id, int name_int, int chargeSlots)
        {

        }

    }
}
