using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public partial class BL :IBL
    {
        public void addStation(int id, int name, double longitude, double lattitude, int chargeSlots)
        {
            DalObject.DalObject temp = new DalObject.DalObject();
            temp.addStaion(id, name, longitude, lattitude, chargeSlots);
        }

    }
}
