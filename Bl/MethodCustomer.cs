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
            IDAL.DO.Customer dalCustomer = new IDAL.DO.Customer()
            {
             Id= CustomerToAdd.Id,
             Name= CustomerToAdd.Name,
             Phone= CustomerToAdd.Phone,
             Longitude= CustomerToAdd.Address.Longitude,
             Lattitude= CustomerToAdd.Address.Lattitude,
            };

            dal.addCustomer(dalCustomer);

        }
    }
}
