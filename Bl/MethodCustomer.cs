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
                throw new AddingProblemException("The customer alradey exist in the system", ex);
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
            catch (IDAL.DO.DoesntExistException ex)
            {

                throw new GetDetailsProblemException("The customer doesn't exist in the system", ex);
            }
                
        }

        public IEnumerable <StationToList> getAllCustomers()
        {


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

            catch (IDAL.DO.DoesntExistException ex)
            {
                throw new GetDetailsProblemException ("The customer doesn't exist in the system", ex);
            }
            catch (IDAL.DO.AlreadyExistException ex)
            {
                throw new AddingProblemException("The customer already exist in the system", ex);
            }


        }

    }
}
