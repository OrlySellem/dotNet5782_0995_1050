using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Parcel
        {
            public int Id { get; set; }

            public int Senderld { get; set; }

            public int Targetld { get; set; }

            public WeightCategories Weight { get; set; }

            public Priorities Priority { get; set; }

            public DateTime Requested { get; set; }

            public int Droneld { get; set; }

            public DateTime Scheduled { get; set; }

            public DateTime PickedUp { get; set; }

            public DateTime Delivered { get; set; }

            public override string ToString()
            {
                return string.Format("Id is:{0}\t  Senderld is:{1}\t  Targetld is:{2}\t  Weight is:{3}\t Priority is:{4}\t" +
                    "Requested is:{5}\t Droneld is:{6}\t Scheduled is:{7}\t PickedUp is:{8}\t Delivered is:{9}\t"
                    ,Id, Senderld, Targetld, Priority, Requested, Droneld, Scheduled, PickedUp, Delivered);
            }
        }
    }
}
