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
            //Ask the user to insert the station's details
            Console.WriteLine("Please enter station's id:");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Please enter station's name:");
            int name = int.Parse(Console.ReadLine());
            Console.WriteLine("Please enter the station's longitude");
            double longitude = double.Parse(Console.ReadLine());
            Console.WriteLine("Please enter the station's lattitude");
            double lattitude = double.Parse(Console.ReadLine());
            Console.WriteLine("Please enter the station's chargeSlots");
            int chargeSlots = int.Parse(Console.ReadLine());
            Console.WriteLine();

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
            //Ask the user to insert the drone's details
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
            Console.WriteLine();

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
            //Ask the user to insert the customer's details
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
            Console.WriteLine();


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
            //Ask the user to insert the parcel's details
            Console.WriteLine("Please enter sender's id:");
            int senderld = int.Parse(Console.ReadLine());
            Console.WriteLine("Please enter target's id:");
            int targetld = int.Parse(Console.ReadLine());
            Console.WriteLine("Please enter drone's weight categories - 0 for light, 1 for medium, 2 for heavy:");
            int maxWeight = int.Parse(Console.ReadLine());
            Console.WriteLine("Please enter the delivery's priority - 0 for normal, 1 for fast, 2 for emergency:");
            int priority = int.Parse(Console.ReadLine());
            Console.WriteLine();

            DataSource.parcels[DataSource.Config.index_parcels++] = new Parcel()
            {
                Id = DataSource.Config.idParcel,
                Senderld = senderld,
                Targetld = targetld,
                Weight = (WeightCategories)maxWeight,
                Priority = (Priorities)priority,
                Droneld = 0,
                Requested = DateTime.Now,
                Scheduled = new DateTime(01, 01, 0001),
                PickedUp = new DateTime(01, 01, 0001),
                Delivered = new DateTime(01, 01, 0001),
            };
            Console.WriteLine("Your parcel's id is:{0}\n", DataSource.Config.idParcel++);
        }

        public int findIndexParcel(int id)
        {
            for(int i=0; i<DataSource.parcels.Length; i++)
            {
                if(DataSource.parcels[i].Id==id)
                {
                    return i;//return the index
                }
            }
            return -1;//if the id isn't esixt in the parcel's array
        }

        public int findIndexDrone(int id)
        {
            for (int i = 0; i < DataSource.drones.Length; i++)
            {
                if (DataSource.drones[i].Id == id)
                    return i;
            }

            return -1;
        }

        public int findIndexStation(int id)
        {
            for (int i = 0; i < DataSource.drones.Length; i++)
            {
                if (DataSource.stations[i].Id == id)
                    return i;
            }
            return -1;
        }

        public int findIndexCustomer(int id)//loop to find the customer acordding to ID 
        {
            for (int i = 0; i < DataSource.customers.Length; i++)
            {
                if (DataSource.customers[i].Id == id)
                {
                    return i;
                }
            }

            return -1;
        }
       
        public int checkIndexParcel()
        {
            int indexParcel, idParcel;
            do
            {
                Console.WriteLine("Please enter parcel's id:");
                idParcel = int.Parse(Console.ReadLine());
                indexParcel = findIndexParcel(idParcel);
                if (indexParcel == -1)
                    Console.WriteLine("The parcel isn't exist\n");

            }
            while (indexParcel == -1);
            return indexParcel;
        }
        public int checkIndexDrone()
        {
            int indexDrone, idDrone;
            do
            {
                Console.WriteLine("Please enter drone's id:");
                idDrone = int.Parse(Console.ReadLine());
                indexDrone = findIndexDrone(idDrone);
                if (indexDrone == -1)
                    Console.WriteLine("The drone isn't exist\n");

            }
            while (indexDrone == -1);
            return indexDrone;
        }
        public int checkIndexStation()
        {
            Console.WriteLine("Choose one of the following stations with free charging staion\n");
            print_stations_with_freeDroneCharge();
            int indexStation, idStation;
            do
            {
                Console.WriteLine("Please enter station's id:");
                idStation = int.Parse(Console.ReadLine());
                indexStation = findIndexStation(idStation);
                if (indexStation == -1)
                    Console.WriteLine("The station isn't exist\n");

            }
            while (indexStation == -1);
            return indexStation;

        }

        public void assign_parcel_drone()//assign parcel to drone
        {
            int indexParcel = checkIndexParcel();
            int indexDrone = checkIndexDrone();
           
            if (DataSource.drones[indexDrone].Status == DroneStatuses.available)
            {
                DataSource.parcels[indexParcel].Droneld = DataSource.drones[indexDrone].Id;
                DataSource.parcels[indexParcel].Scheduled = DateTime.Now;
                DataSource.drones[indexDrone].Status = DroneStatuses.delivery;
            }
            else
            {
                Console.WriteLine("The drone isn't available\n");
            }
        }

        public void drone_pick_parcel()//pick up parcel by drone
        {
            int indexParcel = checkIndexParcel();
            int indexDrone = checkIndexDrone();
            DataSource.drones[indexDrone].Status = DroneStatuses.delivery;
            DataSource.parcels[indexParcel].PickedUp = DateTime.Now;
        }
        public void delivery_arrive_toCustomer()//The delivery arrived to the customer
        {
            int indexParcel = checkIndexParcel();
            int indexDrone = checkIndexDrone();
            DataSource.drones[indexDrone].Status = DroneStatuses.available;
            DataSource.parcels[indexParcel].Delivered = DateTime.Now;
        }

        public void chargingDrone()
        {
            
            int indexDrone = checkIndexDrone();// הכנסת רחפן         
            int indexStation = checkIndexStation();//הכנסת תחנת בסיס 
            while (DataSource.stations[indexStation].ChargeSlots == 0)//אם אין תחנות הטענה פנויות תבחר תחנה חדשה  
            {
                Console.WriteLine("There aren't available charge slots\n");
                indexStation = checkIndexStation();//הכנסת תחנת בסיס 
            }

            DataSource.drones[indexDrone].Status = DroneStatuses.maintenance;//שינוי סטטוס הרחפן
            DataSource.stations[indexStation].ChargeSlots--;//הקטנת מספר עמדות הטענה
            for(int i=0; i< DataSource.DroneCharge.Length;i++)//תעבור על מערך הCD וכאשר נגיע למקום פנוי....
            {
                if (DataSource.DroneCharge[i].flag == false)
                {//תיצור לתוכו DC חדש 
                    DataSource.DroneCharge[i].Droneld = DataSource.drones[indexDrone].Id;
                    DataSource.DroneCharge[i].Stationld = DataSource.stations[indexStation].Id;
                    DataSource.DroneCharge[i].flag = true;
                    break;
                }

            }
        }

        public void freeDroneCharge()
        {
            int indexDrone = checkIndexDrone();//הכנסת רחפן תקין והבאת המקום שלו
            int indexStation = checkIndexStation();//הכנסת תחנה תקינה והבאת המקום שלו 

            for(int i=0; i < DataSource.DroneCharge.Length; i++)//תעבור על מערך הCD וכאשר נגיע למקום פנוי....
            {
                if (DataSource.DroneCharge[i].Stationld == DataSource.stations[indexStation].Id)//אם מצאנו שמות של תחנות בסיס שוות
                {
                    if (DataSource.DroneCharge[i].Droneld == DataSource.drones[indexDrone].Id)
                    {
                        DataSource.DroneCharge[i].flag = false;
                        break;
                    }                                   
                }

            }
            DataSource.stations[indexStation].ChargeSlots++;
            DataSource.drones[indexDrone].Status = DroneStatuses.available;
            DataSource.drones[indexDrone].Battery = 100;
        }

        public void printStation(int id)
        {
            for (int i = 0; i < DataSource.stations.Length; i++)
            {
                if (DataSource.stations[i].Id == id)
                {
                    Console.WriteLine(DataSource.stations[i].ToString());
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
                    Console.WriteLine(DataSource.drones[i].ToString());
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
                    Console.WriteLine(DataSource.customers[i].ToString());
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
                    Console.WriteLine(DataSource.parcels[i].ToString());
                    break;
                }
            }
        }

        public void printAllStations()
        {
            foreach (Station item in DataSource.stations)
            {
                if (item.Id == 0)
                    break;
                Console.WriteLine(item.ToString());
            }

        }
        public void printAllDrones()
        {
            foreach (Drone item in DataSource.drones)
            {
                if (item.Id == 0)
                    break;
                Console.WriteLine(item.ToString());
            }

        }

        public void printAllCustomers()
        {
            foreach (Customer item in DataSource.customers)
            {
                if (item.Id == 0)
                    break;
                Console.WriteLine(item.ToString());
            }

        }

        public void printAllParcels()
        {
            foreach (Parcel item in DataSource.parcels)
            {
                if (item.Id == 0)
                    break;
                Console.WriteLine(item.ToString());
            }
        }

        public void print_unconnected_parcels_to_Drone()
        {
            foreach (Parcel item in DataSource.parcels)
            {
                if (item.Droneld == 0 && item.Id!=0)
                    Console.WriteLine(item.ToString());
            }
        }

        public void print_stations_with_freeDroneCharge()
        {
            foreach (Station item in DataSource.stations)
            {
                if (item.ChargeSlots > 0 && item.Id != 0)
                    Console.WriteLine(item.ToString());
            }
        }

    }
}

