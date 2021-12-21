using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Drone
    {

        public int Id { get; set; }

        public string Model { get; set; }

        public WeightCategories MaxWeight { get; set; }

        public override string ToString()
        {
            return string.Format("\nId is:{0}\nModel is:{1}\nMaxWeight is:{2}\n", Id, Model, MaxWeight);
        }

    }
}
