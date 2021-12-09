/* Ester Shmuel ID:318968468 && Orly Sellem ID:315208728
  Lecturer: Adina Milston
  Course: .Dot Net
  Exercise: 2
  The purpose of DataSource is to define and initialize the entities 
 */
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
        //define array to all of the entities (different size's initialize) 
        internal static Random rand = new Random();

        internal static List <Drone> drones = new List <Drone>();

        internal static List<Station> stations = new List<Station>();

        internal static List<Customer> customers = new List<Customer>();

        internal static List<Parcel> parcels = new List<Parcel>();

        internal static List<DroneCharge> dronesCharge = new List<DroneCharge>();

        internal class Config
        {
            internal static int idParcel = 1;//Runner parcel's ID number 

            //Static features for drone power consumption

            internal static double available = 1;
            internal static double lightWeight = 5;//Lightweight issue
            internal static double mediumWeight = 10;//MediumWeight issue
            internal static double heavyWeight = 15;//Heavyweight issue
            internal static double Drone_charging_speed=40;//Drone charging speed in percentage per hour
        }
          
        public static void Initialize()
        {
            //Initialize 5 drones
            string [] ModelArr = { "1G", "2G", "3G", "4G", "5G" };
            for (int i = 0; i < 5; i++)
            {
                Drone temp = new Drone()
                {
                    Id= rand.Next(1000, 9999),
                    Model= ModelArr[i], 
                    MaxWeight= (WeightCategories)rand.Next(0, 2)                  
                };
                drones.Add(temp);
            }
            //Initialize 2 stations
            for (int i = 0; i < 2; i++)
            {
                Station temp = new Station()
                {
                    Id = rand.Next(10000,99999),
                    Name=i,
                    Longitude=rand.Next(1000,4000),
                    Lattitude=rand.Next(1000,4000),
                    ChargeSlots =rand.Next(1,10), 
                };
                stations.Add(temp);
            }
            //Initialize 10 customers
            for (int i = 0; i < 10; i++)
            {
                string newName="";
                for (int j = 0; j < 5; j++)
                {
                    int a = rand.Next(97, 122);
                    newName += (char)a;
                }
                Customer temp = new Customer()
                {
                    Id = rand.Next(100000000, 999999999),
                    Name = newName,
                    Phone = "05" + rand.Next(0, 4) + "-" + rand.Next(10000000, 99999999),
                    Longitude=rand.Next(-180, 180),
                    Lattitude=rand.Next(-90,90),
                };
                customers.Add(temp);

            }
            //Initialize 10 parcels
            for (int i = 0; i < 10; i++)
            {
                int Senderld = customers[rand.Next(9)].Id;
                int Targetld;
                do
                {
                    Targetld = customers[rand.Next(9)].Id;
                }
                while (Targetld == Senderld);
                
                Parcel temp = new Parcel()
                {
                    Id = Config.idParcel++,
                    Senderld = Senderld,
                    Targetld = Targetld,
                    Weight =(WeightCategories)rand.Next(0,2),
                    Priority=(Priorities)rand.Next(0,2),
                    Droneld = 0,
                    Requested = new DateTime(),
                    Scheduled = new DateTime(01, 01, 0001),
                    PickedUp = new DateTime(01, 01, 0001),
                    Delivered = new DateTime(01, 01, 0001),
                };

                parcels.Add(temp);
            }

        }


    }
}
