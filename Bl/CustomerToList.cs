using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class CustomerToList
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public int Num_of_sented_and_provided_parcels { get; set; }

        public int Num_of_sented_and_unprovided_parcels { get; set; }

        public int Num_of_received_parcels { get; set; }

        public int Num_of_parcels_onTheWay_toCustomer { get; set; }

        public override string ToString()
        {
            return string.Format("\nId is:{0}\nName is:{1}\nPhone number is:{2}\nNum of provided parcels is:{3}\nNum of unprovided parcels is:{4}\nNum of received parcels is:{5}\nNum of parcels on the way to customer is:{6}\n", Id, Name, Phone, Num_of_sented_and_provided_parcels, Num_of_sented_and_unprovided_parcels, Num_of_received_parcels, Num_of_parcels_onTheWay_toCustomer);
        }

    }

}
