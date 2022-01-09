using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class StationToList
    {
        public int Id { get; set; }

        public int Name { get; set; }

        public int ChargeSlotsFree { get; set; }

        public int ChargeSlotsFull { get; set; }

        public override string ToString()
        {
            return string.Format("\nId is:{0}\nName is:{1}\nThe number of charge slots that free is:{2}\nThe number of charge slots that full is:{3}\n", Id, Name, ChargeSlotsFree, ChargeSlotsFull);
        }
    }

}
