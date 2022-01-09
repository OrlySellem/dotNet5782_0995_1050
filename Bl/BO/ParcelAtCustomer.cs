using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ParcelAtCustomer
    {
        public int Id { get; set; }

        public WeightCategories Weight { get; set; }

        public Priorities Priority { get; set; }

        public ParcelStatus ParcelStatus { get; set; }

        public Customer SenderOrTarget { get; set; }

        public override string ToString()
        {
            return string.Format("Id is:{0}\nWeight is:{1}\nPriority is:{2}\nThe status of parcel is:{3}\nThe customer is:{4}\n", Id, Weight, Priority, ParcelStatus, SenderOrTarget);
        }

    }

}
