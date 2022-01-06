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
    public partial class BL : IBL
    {
        #region CRUD
        //Create
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addParcel(Parcel ParcelToAdd)
        {
            lock (dal)
            {
                try
                {
                    DO.Parcel dalParcel = new DO.Parcel()
                    {
                        Id = 0,
                        Senderld = ParcelToAdd.Senderld,
                        Targetld = ParcelToAdd.Targetld,
                        Weight = (DO.WeightCategories)ParcelToAdd.Weight,
                        Priority = (DO.Priorities)ParcelToAdd.Priority,
                        Requested = DateTime.Now,
                        Droneld = 0,
                        Scheduled = null,
                        PickedUp = null,
                        Delivered = null
                    };

                    dal.addParcel(dalParcel);
                }
                catch (DO.AlreadyExistException ex)
                {

                    throw new AlreadyExistException(ex.Message);
                }

            }

        }

        // Read
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel getParcel(int id)
        {
            lock (dal)
            {
                try
                {
                    DO.Parcel p = dal.getParcel(id);


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
                catch (DO.DoesntExistentObjectException ex)
                {
                    throw new DoesntExistentObjectException(ex.Message);
                }

            }

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> getAllParcels(Predicate<ParcelToList> predicate = null)
        {
            lock (dal)
            {
                IEnumerable<DO.Parcel> ParcelList_dal = dal.getParcels().ToList();
                List<ParcelToList> ParcelList_bl = new List<ParcelToList>();

                foreach (var parcelItem in ParcelList_dal)
                {
                    ParcelToList addParcel = new ParcelToList()
                    {
                        Id = parcelItem.Id,
                        Senderld = parcelItem.Senderld,
                        Targetld = parcelItem.Targetld,
                        Weight = (WeightCategories)parcelItem.Weight,
                        Priority = (Priorities)parcelItem.Priority,
                    };

                    if (parcelItem.Requested != null && parcelItem.Scheduled == null)
                        addParcel.ParcelStatus = ParcelStatus.requested;
                    if (parcelItem.Scheduled != null && parcelItem.PickedUp == null)
                        addParcel.ParcelStatus = ParcelStatus.scheduled;
                    if (parcelItem.PickedUp != null && parcelItem.Delivered == null)
                        addParcel.ParcelStatus = ParcelStatus.PickedUp;
                    if (parcelItem.Delivered != null)
                        addParcel.ParcelStatus = ParcelStatus.Delivered;



                    ParcelList_bl.Add(addParcel);

                }

                return ParcelList_bl.FindAll(x => predicate == null ? true : predicate(x));

            }
        }

        //Update
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> ParcelDoesntAssignToDrone()
        {
            lock (dal)
            {
                var parcelList_dal = dal.print_unconnected_parcels_to_Drone();
                List<ParcelToList> ParcelList_bl = new List<ParcelToList>();

                foreach (var parcelItem in parcelList_dal)
                {
                    ParcelToList addParcel = new ParcelToList()
                    {
                        Id = parcelItem.Id,
                        Senderld = parcelItem.Senderld,
                        Targetld = parcelItem.Targetld,
                        Weight = (WeightCategories)parcelItem.Weight,
                        Priority = (Priorities)parcelItem.Priority,
                        ParcelStatus = ParcelStatus.requested
                    };

                    ParcelList_bl.Add(addParcel);
                }

                return ParcelList_bl;
            }
           
        }

        //Delete
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deleteFromParcels(int IDparcelToDel)
        {
            lock (dal)
            {
                DO.Parcel parcel = dal.getParcel(IDparcelToDel);
                dal.delFromParcels(parcel);
            }
           
        }

        #endregion CRUD
    }
}
