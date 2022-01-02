using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DalApi
{
    public class DalFactory
    {
        public static IDal GetDal(string str)
        {
            switch (str)
            {
                case "1":
                    return DalObject.DalObject.Instance;
                case "2":
                //

                default:
                    throw new stringException();
            }


        }
    }
}

