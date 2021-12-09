using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public partial class BL: IBL
    {

        public void addCustomer(Customer CustomerToAdd)
        {
            try
            {
                IDAL.DO.Customer dalCustomer = new IDAL.DO.Customer()
                {
                    Id = CustomerToAdd.Id,
                    Name = CustomerToAdd.Name,
                    Phone = CustomerToAdd.Phone,
                    Longitude = CustomerToAdd.Address.Longitude,
                    Lattitude = CustomerToAdd.Address.Lattitude,
                };

                dal.addCustomer(dalCustomer);
            }
            catch (IDAL.DO.AlreadyExistException ex)
            {
                throw new AlreadyExistException("", ex);
            }
           
        }

        public Customer getCustomer(int id)
        {

            try
            {
                IDAL.DO.Customer c = dal.getCustomer(id);

                Location address = new Location()
                {
                    Longitude = c.Longitude,
                    Lattitude = c.Lattitude
                };

                return new BO.Customer//have to add fromToCustomer list and ToCustomer!! - לעשות
                {
                    Id = c.Id,

                    Name = c.Name,

                    Phone = c.Phone,

                    Address = address,
                };

            }
            catch (IDAL.DO.DoesntExistentObjectException ex)
            {

                throw new DoesntExistentObjectException("",ex);
            }
                
        }


        public IEnumerable<CustomerToList> getAllCustomers()
        {
            IEnumerable<IDAL.DO.Customer> CustomerList_dal = dal.getAllCustomer();
            List<CustomerToList> CustomerList_bl = new List<CustomerToList>();

            IEnumerable<IDAL.DO.Parcel> ParcelsList_dal = dal.getAllParcels();


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
                        if(parcelItem.Delivered != new DateTime(01, 01, 0001))
                            send_and_Deliver_conster++;

                        if (parcelItem.Delivered == new DateTime(01, 01, 0001) && parcelItem.PickedUp != new DateTime(01, 01, 0001))
                            send_and_not_provided_conster++;
                    }
                    if (parcelItem.Targetld == itemCustomer.Id)
                    {
                        if (parcelItem.Delivered != new DateTime(01, 01, 0001))
                            target_and_Deliver_conster++;

                        if (parcelItem.Delivered == new DateTime(01, 01, 0001) && parcelItem.PickedUp != new DateTime(01, 01, 0001))
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
                    Num_of_parcels_onTheWay_toCustomer= target_and_not_provided_conster
                };
                CustomerList_bl.Add(addCustomer);
            }

            return CustomerList_bl;
        }

        public void updateCustomer(int idCustomer, string newName, string newPhone)
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

            catch (IDAL.DO.DoesntExistentObjectException ex)
            {
                throw new DoesntExistentObjectException("",ex);
            }
            catch (IDAL.DO.AlreadyExistException ex)
            {
                throw new AlreadyExistException("", ex);
            }


        }
    }
}
