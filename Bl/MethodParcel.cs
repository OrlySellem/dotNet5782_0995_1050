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
                    Droneld = 0,
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

        public IEnumerable<ParcelToList> getAllParcels()
        {
              IEnumerable<IDAL.DO.Parcel> ParcelList_dal = dal.getAllParcels();
              List<ParcelToList> ParcelList_bl = new List<ParcelToList>();
       
            foreach (var parcelItem in ParcelList_dal)
            {
                ParcelStatus Status;
                if (parcelItem.Requested != new DateTime(01, 01, 0001) && parcelItem.Scheduled == new DateTime(01, 01, 0001))
                    Status = ParcelStatus.requested;
                if (parcelItem.Scheduled != new DateTime(01, 01, 0001) && parcelItem.PickedUp == new DateTime(01, 01, 0001))
                    Status = ParcelStatus.scheduled;
                if (parcelItem.PickedUp != new DateTime(01, 01, 0001) && parcelItem.Delivered == new DateTime(01, 01, 0001))
                    Status = ParcelStatus.PickedUp;
                if (parcelItem.Delivered == new DateTime(01, 01, 0001))
                    Status = ParcelStatus.Delivered;

                ParcelToList addParcel = new ParcelToList()
                {
                    Id = parcelItem.Id,
                    Senderld = parcelItem.Senderld,
                    Targetld = parcelItem.Targetld,
                    Weight = (WeightCategories)parcelItem.Weight,
                    Priority = (Priorities)parcelItem.Priority,
                    ParcelStatus = Status
                };
                ParcelList_bl.Add(addParcel);

            }

            return ParcelList_bl;
        }


    }


}
