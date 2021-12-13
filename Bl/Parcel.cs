using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Parcel
        {
            public int Id { get; set; }

            public int Senderld { get; set; }

            public int Targetld { get; set; }

            public WeightCategories Weight { get; set; }

            public Priorities Priority { get; set; }

            public DateTime? Requested { get; set; }

            public int Droneld { get; set; }

            public DateTime? Scheduled { get; set; }

            public DateTime? PickedUp { get; set; }

            public DateTime? Delivered { get; set; }

            public override string ToString()
            {
                return string.Format("\nId is:{0}\nSenderld is:{1}\nTargetld is:{2}\nWeight is:{3}\nPriority is:{4}\n" +
                    "Requested is:{5}\nDroneld is:{6}\nScheduled is:{7}\nPickedUp is:{8}\nDelivered is:{9}\n"
                    , Id, Senderld, Targetld, Weight, Priority, Requested, Droneld, Scheduled, PickedUp, Delivered);
            }


        }


    }
}
