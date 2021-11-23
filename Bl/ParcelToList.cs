using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class ParcelToList
        {
            public int Id { get; set; }

            public int Senderld { get; set; }

            public int Targetld { get; set; }

            public WeightCategories Weight { get; set; }

            public Priorities Priority { get; set; }

            public ParcelStatus ParcelStatus { get; set; }

            public override string ToString()
            {
                return string.Format("\nId is:{0}\nSenderld is:{1}\nTargetld is:{2}\nWeight is:{3}\nPriority is:{4}\n" +
                    "Parcel status is:{5}\n", Id, Senderld, Targetld, Weight, Priority, ParcelStatus);
            }

        }

    }
}
