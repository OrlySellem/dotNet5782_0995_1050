using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Customer
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Phone { get; set; }

            public Location Address { get; set; }

            public List <ParcelAtCustomer> FromCustomer { get; set; }

            public List <ParcelAtCustomer> ToCustomer { get; set; }

            public override string ToString()
            {
                return string.Format("\nId is:{0}\nName is:{1}\nPhone is:{2}\naddress is:{3}\nParcel at customer - from customer:{4}\nParcel at customer - to customer:{5}", Id, Name, Phone, Address, FromCustomer, ToCustomer);
            }


        }
    }
}
