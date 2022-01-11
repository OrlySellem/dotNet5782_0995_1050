using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Location
    {

        public double Longitude { get; set; }

        public double Lattitude { get; set; }

        public override string ToString()
        {
                     return string.Format("({0}° {1}′ {2}″ N, {3}° {4}′ {5}″ E) \n", (int)Lattitude,((int)((Lattitude % 1) * 60)), ((int)((((Lattitude % 1) * 60) % 1) * 60 + .5)) ,(int)Longitude, ((int)((Longitude % 1) * 60)), ((int)((((Longitude % 1) * 60) % 1) * 60 + .5)));
          
            //    return string.Format("\nLongitude is:{0}\nLattitude is:{1}\n", Longitude, Lattitude);
        }

    }
}
