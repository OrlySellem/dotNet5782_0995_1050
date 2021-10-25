﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
   public class DalObject
    {
        internal static Random rand = new Random();

        DalObject() { DataSource.Initialize(); }
        public void addStaion() 
        {
            DataSource.stations[DataSource.Config.index_stations++] = new Station()
            {
                Id = rand.Next(10000, 99999),
                Name = rand.Next(1,1000),
                Longitude = rand.Next(1000, 4000),
                Lattitude = rand.Next(1000, 4000),
                ChargeSlots = rand.Next(1, 10),
            }; 

        }

        public void addDrone()
        {
            DataSource.drones[DataSource.Config.index_drones++] = new Drone()
            {
                Id = rand.Next(1000, 9999),
                Model = "5G",
                MaxWeight = (WeightCategories)rand.Next(0, 2),
                Status = (DroneStatuses)rand.Next(0, 2),
                Battery = rand.Next(0, 100)
            };
        }

        public void addCustomer()
        {
            string newName = "";
            for (int j = 0; j < 5; j++)
            {
                int temp = rand.Next(97, 122);
                newName += (char)temp;
            }
            DataSource.customers[DataSource.Config.index_customers++] = new Customer()
            {
                Id = rand.Next(100000000, 999999999),
                Name = newName,
                Phone = "05" + rand.Next(0, 4) + "-" + rand.Next(10000000, 99999999),
                Longitude = rand.Next(-180, 180),
                Lattitude = rand.Next(-90, 90),
            };
        }

        public void addParcel()
        {
            DataSource.parcels[DataSource.Config.index_parcels++] = new Parcel()
            {
                Id = rand.Next(100000, 999999),
                Senderld = rand.Next(100000000, 999999999),
                Targetld = rand.Next(100000000, 999999999),
                Weight = (WeightCategories)rand.Next(0, 2),
                Priority = (Priorities)rand.Next(0, 2),
                Droneld = rand.Next(1000, 9999),
                Requested = new DateTime(rand.Next(2021, 2023), rand.Next(1, 12), rand.Next(1, 31)),
            };
        }
        public void assign_parcel_drone()
        {
            

        }

        
    }
}
