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
        IDAL.DO.IDal dal = new DalObject.DalObject();

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


    }
}
