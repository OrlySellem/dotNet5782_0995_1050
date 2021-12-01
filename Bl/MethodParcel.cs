using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public partial class BL:IBL
    {
        public void addParcel(Parcel ParcelToAdd)
        {
            IDAL.DO.Parcel dalParcel = new IDAL.DO.Parcel()
            {
                Senderld = ParcelToAdd.Senderld,
                Targetld = ParcelToAdd.Targetld,
                Weight = (IDAL.DO.WeightCategories)ParcelToAdd.Weight,
                Priority = (IDAL.DO.Priorities)ParcelToAdd.Priority,
                Requested= DateTime.Now,
                Droneld = ParcelToAdd.Droneld,
                Scheduled = new DateTime(01/01/0001),
                PickedUp= new DateTime(01/01/0001),
                Delivered = new DateTime(01/01/0001)
            };

            dal.addParcel(dalParcel);
        }

        public Parcel getParcel(int id)
        {
            IDAL.DO.Parcel p = dal.getParcel(id);

            Location address = new Location()
            {
                Longitude = s.Longitude,
                Lattitude = s.Lattitude
            };

            return new BO.Station//have to add chargingDrone list!! - לעשות
            {
                Id = p.Id,

                Name = p.Name,

                Address = address,

                ChargeSlots = s.ChargeSlots,
            };
        }
        public IEnumerable<StationToList> getAllParcels()
        {


        }
    }


}
