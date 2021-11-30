using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public partial class BL:IBL
    {
        IDAL.DO.IDal dal = new DalObject.DalObject();

        //○	אובייקט BLimp יתחזק רשימת רחפנים (ע"פ הישות הלוגיות "רחפן לרשימה")
        public static List <DroneToList> drones;


    }
}
