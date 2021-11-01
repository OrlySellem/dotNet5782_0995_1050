using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
   static class DataSource
    {
        internal static Random rand = new Random();

        internal static Drone[] drones = new Drone[10];

        internal static Station[] stations = new Station[5];

        internal static Customer[] customers = new Customer[100];

        internal static Parcel[] parcels = new Parcel[1000];

        internal static DroneCharge[] DroneCharge = new DroneCharge[10];

        internal class Config
        {
            internal static int idParcel=1;

            internal static int index_drones = 0;

            internal static int index_stations = 0;

            internal static int index_customers = 0;

            internal static int index_parcels = 0;

            internal static int index_droneCharge = 0;
        }

        public static void Initialize()
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

            for (int i = 0; i < 2; i++)
            {
                stations[Config.index_stations++] = new Station()
                {
                    Id = rand.Next(10000,99999),
                    Name=i,
                    Longitude=rand.Next(1000,4000),
                    Lattitude=rand.Next(1000,4000),
                    ChargeSlots =rand.Next(1,10), 
                };
            }

            for (int i = 0; i < 10; i++)
            {
                string newName="";
                for (int j = 0; j < 5; j++)
                {
                    int temp = rand.Next(97, 122);
                    newName += (char)temp;
                }
                customers[Config.index_customers++] = new Customer()
                {
                    Id = rand.Next(100000000, 999999999),
                    Name = newName,
                    Phone = "05" + rand.Next(0, 4) + "-" + rand.Next(10000000, 99999999),
                    Longitude=rand.Next(-180, 180),
                    Lattitude=rand.Next(-90,90),
                };
            }
           
            for (int i = 0; i < 10; i++)
            {
                parcels[Config.index_parcels++] = new Parcel()
                {
                    Id=Config.idParcel++,
                    Senderld=rand.Next(100000000,999999999),
                    Targetld=rand.Next(100000000,999999999),
                    Weight=(WeightCategories)rand.Next(0,2),
                    Priority=(Priorities)rand.Next(0,2),
                    Droneld = rand.Next(1000, 9999),
                    Requested= new DateTime(),
                    Scheduled = new DateTime(01, 01, 0001),
                    PickedUp = new DateTime(01, 01, 0001),
                    Delivered = new DateTime(01, 01, 0001),
                };
            }

        }


    }
}
