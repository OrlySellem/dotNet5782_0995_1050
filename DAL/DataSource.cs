using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    class DataSource
    {
        internal static Random rand = new Random();

        internal static Drone[] drones = new Drone[10];

        internal static Station[] stations = new Station[5];

        internal static Customer[] customers = new Customer[100];

        internal static Parcel[] parcels = new Parcel[1000];

        internal class Config
        {
            internal static int index_drones = 0;

            internal static int index_stations = 0;

            internal static int index_customers = 0;

            internal static int index_parcels = 0;
        }

        static void Initialize()
        {
            string[] ModelArr = { "1G", "2G", "3G", "4G", "5G" };
            for (int i = 0; i < 5; i++)
            {
                drones[Config.index_drones++] = new Drone()
                {
                    Id = rand.Next(1000, 9999),
                    Model = ModelArr[i],
                    MaxWeight = (WeightCategories)rand.Next(0, 2),
                    Status = (DroneStatuses)rand.Next(0, 2),
                    Battery = rand.Next(0,100)
                };
            }




        }


    }
}
