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
        public void addParcel(Parcel ParcelToAdd)
        {
            try
            {
                IDAL.DO.Parcel dalParcel = new IDAL.DO.Parcel()
                {
                    Senderld = ParcelToAdd.Senderld,
                    Targetld = ParcelToAdd.Targetld,
                    Weight = (IDAL.DO.WeightCategories)ParcelToAdd.Weight,
                    Priority = (IDAL.DO.Priorities)ParcelToAdd.Priority,
                    Requested = DateTime.Now,
                    Droneld = ParcelToAdd.Droneld,
                    Scheduled = new DateTime(01 / 01 / 0001),
                    PickedUp = new DateTime(01 / 01 / 0001),
                    Delivered = new DateTime(01 / 01 / 0001)
                };

                dal.addParcel(dalParcel);
            }
            catch (IDAL.DO.AlreadyExistException ex)
            {

                throw new AddingProblemException("The parcel already exist", ex);
            }

        }

        public Parcel getParcel(int id)
        {
            try
            {
                IDAL.DO.Parcel p = dal.getParcel(id);


                return new BO.Parcel//have to add chargingDrone list!! - לעשות
                {
                    Id = p.Id,

                    Senderld = p.Senderld,

                    Targetld = p.Targetld,

                    Weight = (BO.WeightCategories)p.Weight,

                    Priority = (BO.Priorities)p.Priority,

                    Requested = p.Requested,

                    Droneld = p.Droneld,

                    Scheduled = p.Scheduled,

                    PickedUp = p.PickedUp,

                    Delivered = p.Delivered
                };
            }
            catch (IDAL.DO.DoesntExistException ex)
            {
                throw new GetDetailsProblemException("The parcel doesn't exist in the system", ex);
            }
           
        }

        public IEnumerable<StationToList> getAllParcels()
        {


        }


    }


}
