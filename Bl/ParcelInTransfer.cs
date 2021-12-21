using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace BO
    {
        public class ParcelInTransfer
        {
            public int Id { get; set; }

            public bool Status { get; set; }

            public Priorities Priority { get; set; }

            public WeightCategories Weight { get; set; }

            public CustomerInParcel Sender { get; set; }

            public CustomerInParcel Target { get; set; }

            public Location PickedUp { get; set; }

            public Location DeliveryTarget { get; set; }

            public double distanceDelivery { get; set; }

            public override string ToString()
            {
                return string.Format("Id is:{0}\nStatus is:{1}\nPriority is:{2}\nWeight is:{3}\nSender is:{4}\n" +
                    "Target is:{5}\nThe pick up location is:{6}\nThe targets location is:{7}\nThe delivery's distance is:{8}\n"
                    , Id, Status, Priority, Weight, Sender, Target, PickedUp, DeliveryTarget, distanceDelivery);
            }
        }
   

    }

