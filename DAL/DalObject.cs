using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public class DalObject//מחלקה שבתוכה כל המתודות שיעסקו בהבאת נתונים
    {
        internal static Random rand = new Random();

        public DalObject()
        { 
            DataSource.Initialize();
        }

        public void addStaion()
        {
            Console.WriteLine("Please enter station's id:");
            int id=int.Parse(Console.ReadLine());
            Console.WriteLine("Please enter station's name:");
            int name= int.Parse(Console.ReadLine());
            Console.WriteLine("Please enter the station's longitude");
            double longitude = double.Parse(Console.ReadLine());
            Console.WriteLine("Please enter the station's lattitude");
            double lattitude= double.Parse(Console.ReadLine());
            Console.WriteLine("Please enter the station's chargeSlots");
            int chargeSlots= int.Parse(Console.ReadLine());

            DataSource.stations[DataSource.Config.index_stations++] = new Station()
            {
                Id = id,
                Name = name,
                Longitude = longitude,
                Lattitude = lattitude,
                ChargeSlots = chargeSlots,
            };

        }

        public void addDrone()//To add drone
        {
            Console.WriteLine("Please enter drone's id:");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Please enter drone's model:");
            string model = Console.ReadLine();
            Console.WriteLine("Please enter drone's weight categories - 0 for light, 1 for medium, 2 for heavy:");
            int maxWeight = int.Parse(Console.ReadLine());
            Console.WriteLine("Please enter drone's status - 0 for available, 1 for maintenance, 2 for delivery:");
            int status = int.Parse(Console.ReadLine());
            Console.WriteLine("Please enter drone's battery:");
            int battery = int.Parse(Console.ReadLine());

            DataSource.drones[DataSource.Config.index_drones++] = new Drone()
            {
                Id = id,
                Model = model,
                MaxWeight = (WeightCategories)maxWeight,
                Status = (DroneStatuses)status,
                Battery = battery
            };
        }

        public void addCustomer()//To add customer
        {
            Console.WriteLine("Please enter your id:");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Please enter your name:");
            string name = Console.ReadLine();
            Console.WriteLine("Please enter your phone:");
            string phone = Console.ReadLine();
            Console.WriteLine("Please enter lattitude:");
            double lattitude = double.Parse(Console.ReadLine());
            Console.WriteLine("Please enter longitude:");
            double longitude = double.Parse(Console.ReadLine());


            DataSource.customers[DataSource.Config.index_customers++] = new Customer()
            {
                Id = id,
                Name = name,
                Phone = phone,
                Lattitude = lattitude,
                Longitude = longitude
            };
        }

        public void addParcel()
        {
            Console.WriteLine("Please enter parcel's id:");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Please enter sender's id:");
            int senderld = int.Parse(Console.ReadLine());
            Console.WriteLine("Please enter target's id:");
            int targetld = int.Parse(Console.ReadLine());
            Console.WriteLine("Please enter drone's weight categories - 0 for light, 1 for medium, 2 for heavy:");
            int maxWeight = int.Parse(Console.ReadLine());
            Console.WriteLine("Please enter the delivery's priority - 0 for normal, 1 for fast, 2 for emergency:");
            int priority = int.Parse(Console.ReadLine());

            DataSource.parcels[DataSource.Config.index_parcels++] = new Parcel()
            {
                Id = id,
                Senderld = senderld,
                Targetld = targetld,
                Weight = (WeightCategories)maxWeight,
                Priority = (Priorities)priority,
                Droneld = 0,
                Requested = DateTime.Now,
            };
        }

        public void assign_parcel_drone(ref Parcel p, ref Drone d)//assign parcel to drone
        {
            p.Droneld = d.Id;
            p.Scheduled = DateTime.Now;
        }


        public void drone_pick_parcel(ref Parcel p, ref Drone d)//איסוף חבילה ע"י רחפן
        {
            d.Status = DroneStatuses.delivery;
            p.PickedUp = DateTime.Now;
        }
        public void delivery_arrive_toCustomer(ref Parcel p, ref Drone d)//The delivery arrived to the customer
        {
            d.Status = DroneStatuses.available;
            p.Delivered = DateTime.Now;
        }

        public void chargingDrone(ref Drone d, ref Station s, ref DroneCharge dc)
        {
            dc = new DroneCharge()
            {
                Droneld = d.Id,
                Stationld = s.Id
            };
            d.Status = DroneStatuses.maintenance;
            s.ChargeSlots--;
        }

        public void freeDroneCharge(ref Drone d, ref Station s, ref DroneCharge dc)
        {
            dc.Droneld = 0;
            dc.Stationld = 0;
            s.ChargeSlots++;
            d.Status = DroneStatuses.available;
            d.Battery = 100;
        }

        public void printStation(int id)
        {
            for (int i = 0; i < DataSource.stations.Length; i++)
            {
                if (DataSource.stations[i].Id == id)
                {
                    DataSource.stations[i].ToString();
                    break;
                }
            }
        }

        public void printDrone(int id)
        {
            for (int i = 0; i < DataSource.drones.Length; i++)
            {
                if (DataSource.drones[i].Id == id)
                {
                    DataSource.drones[i].ToString();
                    break;
                }
            }
        }

        public void printCustomer(int id)
        {
            for (int i = 0; i < DataSource.customers.Length; i++)
            {
                if (DataSource.customers[i].Id == id)
                {
                    DataSource.customers[i].ToString();
                    break;
                }
            }
        }

        public void printParcel(int id)
        {
            for (int i = 0; i < DataSource.parcels.Length; i++)
            {
                if (DataSource.parcels[i].Id == id)
                {
                    DataSource.parcels[i].ToString();
                    break;
                }
            }
        }

        public void printAllStations(int id)
        {
            foreach (Station item in DataSource.stations)
            {
                item.ToString();
            }
            /*
            for (int i = 0; i < DataSource.stations.Length; i++)
            {
                DataSource.stations[i].ToString();

            }*/
        }

        public void printAllDrones(int id)
        {
            foreach (Drone item in DataSource.drones)
            {
                item.ToString();
            }

            /*for (int i = 0; i < DataSource.drones.Length; i++)
            {
                DataSource.drones[i].ToString();
            }*/
        }

        public void printAllCustomers(int id)
        {
            foreach (Customer item in DataSource.customers)
            {
                item.ToString(); 
            }

            /*for (int i = 0; i < DataSource.customers.Length; i++)
            {
                DataSource.customers[i].ToString();
            }*/
        }

        public void printAllParcels(int id)
        {
            foreach (Parcel item in DataSource.parcels)
            {
                item.ToString();
            }
            /*
            for (int i = 0; i < DataSource.parcels.Length; i++)
            {
                DataSource.parcels[i].ToString();
            }*/
        }
    }
}

