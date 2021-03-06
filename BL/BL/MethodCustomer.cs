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
    partial class BL: IBL
    {

        #region CRUD
        /// <summary>
        /// add customer to the system
        /// </summary>
        /// <param name="CustomerToAdd"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addCustomer(Customer CustomerToAdd)
        {
            lock (dal)
            {
                try
                {

                    DO.Customer dalCustomer = new DO.Customer()
                    {
                        Id = CustomerToAdd.Id,
                        Name = CustomerToAdd.Name,
                        Phone = CustomerToAdd.Phone,
                        Longitude = CustomerToAdd.Address.Longitude,
                        Lattitude = CustomerToAdd.Address.Lattitude,
                    };

                    dal.addCustomer(dalCustomer);
                }
                catch (DO.AlreadyExistException ex)
                {
                    throw new AlreadyExistException(ex.Message);
                }

            }

        }

        // Read
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer getCustomer(int id)
        {
            lock (dal)
            {
                try
                {
                    DO.Customer customerDal = dal.getCustomer(id);

                    Location address = new Location()
                    {
                        Longitude = customerDal.Longitude,
                        Lattitude = customerDal.Lattitude
                    };

                    List <ParcelToList> fromCustomer = (from parcel in getAllParcels()
                                                           where parcel.Senderld == id
                                                           select parcel).ToList();

                    List <ParcelToList> ToCustomer = (from parcel in getAllParcels()
                                                       where parcel.Targetld == id
                                                       select parcel).ToList();

                    List<ParcelAtCustomer> fromCustomerPAC = new List<ParcelAtCustomer>();

                    List<ParcelAtCustomer> ToCustomerPAC = new List<ParcelAtCustomer>();

                    ParcelAtCustomer parcelFromCustomer, parcelToCustomer;

                    foreach (var parcel in fromCustomer)//מעבר על רשימת החבילות שהלקוח שלח
                    {
                        //מציאת לקוח שקיבל את החבילה
                        CustomerToList customer = (from findCustomer in getAllCustomers()
                                             where findCustomer.Id == parcel.Targetld
                                             select findCustomer).FirstOrDefault();

                        CustomerInParcel target = new CustomerInParcel()
                        {
                            Id = customer.Id,
                            Name= customer.Name
                        };

                        parcelFromCustomer = new ParcelAtCustomer()
                        {
                                   Id = parcel.Id,
                                    Weight = parcel.Weight,
                                    Priority = parcel.Priority,
                                    ParcelStatus = parcel.ParcelStatus,
                                   SenderOrTarget= target //מקבל החבילה מהלקוח
                        };

                        fromCustomerPAC.Add(parcelFromCustomer);

                    }


                    foreach (var parcel in ToCustomer)
                    {
                        //מציאת לקוח שקיבל את החבילה
                        CustomerToList customer = (from findCustomer in getAllCustomers()
                                                   where findCustomer.Id == parcel.Senderld
                                                   select findCustomer).FirstOrDefault();

                        CustomerInParcel sender = new CustomerInParcel()
                        {
                            Id = customer.Id,
                            Name = customer.Name
                        };

                         parcelToCustomer = new ParcelAtCustomer()
                        {
                            Id = parcel.Id,
                            Weight = parcel.Weight,
                            Priority = parcel.Priority,
                            ParcelStatus = parcel.ParcelStatus,                          
                            SenderOrTarget = sender   //שולח החבילה ללקוח
                        };

                        ToCustomerPAC.Add(parcelToCustomer);

                    }



                    return new BO.Customer//have to add fromToCustomer list and ToCustomer!! - לעשות
                    {
                        Id = customerDal.Id,

                        Name = customerDal.Name,

                        Phone = customerDal.Phone,

                        Address = address,

                        FromCustomer = fromCustomerPAC,

                        ToCustomer = ToCustomerPAC
                    };

                }
                catch (DO.DoesntExistentObjectException ex)
                {

                    throw new DoesntExistentObjectException(ex.Message);
                }
            }                        
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerToList> getAllCustomers(Predicate <CustomerToList> predicate = null)
        {
            lock (dal)
            {
                IEnumerable<DO.Customer> CustomerList_dal = dal.getCustomers().ToList();
                List<CustomerToList> CustomerList_bl = new List<CustomerToList>();

                IEnumerable<DO.Parcel> ParcelsList_dal = dal.getParcels().ToList();


                foreach (var itemCustomer in CustomerList_dal)
                {
                    int send_and_Deliver_conster = 0;
                    int send_and_not_provided_conster = 0;
                    int target_and_Deliver_conster = 0;
                    int target_and_not_provided_conster = 0;

                    foreach (var parcelItem in ParcelsList_dal)
                    {
                        if (parcelItem.Senderld == itemCustomer.Id)
                        {
                            if (parcelItem.Delivered != null)
                                send_and_Deliver_conster++;

                            if (parcelItem.Delivered == null && parcelItem.PickedUp != null)
                                send_and_not_provided_conster++;
                        }
                        if (parcelItem.Targetld == itemCustomer.Id)
                        {
                            if (parcelItem.Delivered != null)
                                target_and_Deliver_conster++;

                            if (parcelItem.Delivered == null && parcelItem.PickedUp != null)
                                target_and_not_provided_conster++;
                        }
                    }


                    CustomerToList addCustomer = new CustomerToList()
                    {
                        Id = itemCustomer.Id,
                        Name = itemCustomer.Name,
                        Phone = itemCustomer.Phone,
                        Num_of_sented_and_provided_parcels = send_and_Deliver_conster,
                        Num_of_sented_and_unprovided_parcels = send_and_not_provided_conster,
                        Num_of_received_parcels = target_and_Deliver_conster,
                        Num_of_parcels_onTheWay_toCustomer = target_and_not_provided_conster
                    };
                    CustomerList_bl.Add(addCustomer);
                }

                return CustomerList_bl.FindAll(x => predicate == null ? true : predicate(x));
            }
           
        }


        //Update
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateCustomer(int idCustomer, string newName, string newPhone)
        {
            lock (dal)
            {
                try
                {
                    var customerToUpdate = dal.getCustomer(idCustomer);
                    dal.delFromCustomers(customerToUpdate);

                    if (newName != null)
                    {
                        customerToUpdate.Name = newName;
                    }

                    if (newPhone != null)
                    {
                        customerToUpdate.Phone = newPhone;
                    }

                    dal.addCustomer(customerToUpdate);
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
        public void deleteFromCustomers(int IDcustomerToDel)
        {
            lock (dal)
            {
                DO.Customer customer = dal.getCustomer(IDcustomerToDel);
                dal.delFromCustomers(customer);
            }           
        }

        #endregion CRUD
    }
}
