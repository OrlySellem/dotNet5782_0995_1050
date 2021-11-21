/* Ester Shmuel ID:318968468 && Orly Sellem ID:315208728
  Lecturer: Adina Milston
  Course: .Dot Net
  Exercise: 2
  The purpose of DalObject's file is to manages all add / bring / update methods
 */
using System;
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

        public DalObject()
        {
            DataSource.Initialize();
        }

        public void addStaion(int id, int name, double longitude, double lattitude, int chargeSlots)//add new base station
        {
            //Ask the user to insert the station's details
            Station temp = new Station()
            {
                Id = id,
                Name = name,
                Longitude = longitude,
                Lattitude = lattitude,
                ChargeSlots = chargeSlots,
            };

            DataSource.stations.Add(temp);

        }

        public void addDrone(int id, string model, int maxWeight)// add drone
        {

            Drone temp = new Drone()
            {
                Id = id,
                Model = model,
                MaxWeight = (WeightCategories)maxWeight,
            };

            DataSource.drones.Add(temp);

        }

        public void addCustomer(int id, string name, string phone, double longitude, double lattitude)// add customer
        {

            Customer temp = new Customer()
            {
                Id = id,
                Name = name,
                Phone = phone,
                Lattitude = lattitude,
                Longitude = longitude
            };

            DataSource.customers.Add(temp);
        }

        public void addParcel(int senderld, int targetld, int maxWeight, int priority)//add new base percel
        {

            Parcel temp = new Parcel()
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
            Console.WriteLine("Your parcel's id is:{0}\n", DataSource.Config.idParcel++);//Printing what the Parcel number
            DataSource.parcels.Add(temp);
        }

        public int findIndexParcel(int id)//Finds the requested parcel from the arr
        {
            for (int i = 0; i < DataSource.parcels.Count; i++)
            {
                if (DataSource.parcels[i].Id == id)
                {
                    return i;//return the index
                }
            }
            return -1;//if the id isn't esixt in the parcel's array
        }

        public int findIndexDrone(int id)//Finds the requested drone from the arr
        {
            for (int i = 0; i < DataSource.drones.Count; i++)
            {
                if (DataSource.drones[i].Id == id)
                    return i;
            }

            return -1;
        }

        public int findIndexStation(int id)//Finds the requested station from the arr
        {
            for (int i = 0; i < DataSource.drones.Count; i++)
            {
                if (DataSource.stations[i].Id == id)
                    return i;
            }
            return -1;
        }

        public int findIndexCustomer(int id)//loop to find the customer acordding to ID 
        {
            for (int i = 0; i < DataSource.customers.Count; i++)
            {
                if (DataSource.customers[i].Id == id)
                {
                    return i;
                }
            }

            return -1;
        }

        public int checkIndexParcel()//Receives a parcel from the user and make sure that it exists
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
        public void assign_parcel_drone (int idDrone, int idParcel)//assign parcel to drone
        {
            int indexDrone = findIndexDrone(idDrone);
            int indexParcel = findIndexParcel(idParcel);

            if(indexDrone == -1)
                throw "The drone isn't exist\n";

            if (indexParcel == -1)
                throw "The parcel isn't exist\n";


           if (DataSource.drones[indexDrone].Status == DroneStatuses.available)
            {
                DataSource.parcels[indexParcel].Droneld = DataSource.drones[indexDrone].Id;
                DataSource.parcels[indexParcel].Scheduled = DateTime.Now;
                DataSource.drones[indexDrone].Status = DroneStatuses.delivery;
            }
            else
            {
                throw "The drone isn't available\n";
            }
        }

        public void drone_pick_parcel(int idDrone, int idParcel)//pick up parcel by drone
        {
            int indexDrone = findIndexDrone(idDrone);
            int indexParcel = findIndexParcel(idParcel);
         
             if(indexDrone == -1)
                throw "The drone isn't exist\n";

            if (indexParcel == -1)
                throw "The parcel isn't exist\n";

            Parcel p = DataSource.parcels[indexParcel];
            p.PickedUp = DateTime.Now;
            DataSource.parcels[indexParcel] = p;
        }
        public void delivery_arrive_toCustomer(int idDrone, int idParcel)//The delivery arrived to the customer
        {
            int indexDrone = findIndexDrone(idDrone);
            int indexParcel = findIndexParcel(idParcel);
         
             if(indexDrone == -1)
                throw "The drone isn't exist\n";

            if (indexParcel == -1)
                throw "The parcel isn't exist\n";

            Parcel pd = DataSource.parcels[indexParcel];
            pd.Delivered = DateTime.Now;
            DataSource.parcels[indexParcel] = pd;
        }

        public void chargingDrone(int idDrone, int idStation)//Inserts a drone to charg
        {

            int indexDrone = findIndexDrone(idDrone);
            int indexStation = findIndexStation(idStation);

            if(indexDrone == -1)
                throw "The drone isn't exist\n";

            if (indexParcel == -1)
                throw "The parcel isn't exist\n";

            if (DataSource.stations[indexStation].ChargeSlots == 0)//If there are no charge slots available, choose a new station  
            {
                throw "There aren't available charge slots\n";
            }

            Station s = DataSource.stations[indexStation];
            s.ChargeSlots--;//Reduce the number of claim positions
            DataSource.stations[indexStation] = s;

            for (int i = 0; i < DataSource.dronesCharge.Count; i++)
            {
                if (DataSource.dronesCharge[i].flag == false)//In free space inserts the data to DroneCharge
                {
                    DroneCharge d = DataSource.dronesCharge[i];
                    d.Droneld = DataSource.drones[indexDrone].Id;
                    d.Stationld = DataSource.stations[indexStation].Id;
                    d.flag = true;
                    DataSource.dronesCharge[i] = d;
                    break;
                }

            }
        }

        public void freeDroneCharge(int idDrone, int idStation)//Drone release from charging
        {
            int indexDrone = findIndexDrone(idDrone);
            int indexStation = findIndexStation(idStation);

               if(indexDrone == -1)
                throw "The drone isn't exist\n";

            if (indexStation == -1)
                throw "The station isn't exist\n";

            for (int i = 0; i < DataSource.dronesCharge.Count; i++)
            {
                if (DataSource.dronesCharge[i].Stationld == DataSource.stations[indexStation].Id)//We found in charging the requested name of a base station
                {
                    if (DataSource.dronesCharge[i].Droneld == DataSource.drones[indexDrone].Id)//We found in charging the matching requested name of a drone
                    {
                        DroneCharge temp = DataSource.dronesCharge[i];
                        temp.flag = false;
                        DataSource.dronesCharge[i] = temp;
                        break;
                    }
                }

            }
            Station st = DataSource.stations[indexStation];
            st.ChargeSlots++;
            DataSource.stations[indexStation] = st;

        }

        public void printStation(int id)//print  the requested Station
        {
            foreach (var item in DataSource.stations)
            {
                if (item.Id == id)
                {
                    Console.WriteLine(item.ToString());
                    break;
                }
            }

        }

        public void printDrone(int id)//print the requested Drone
        {
            foreach (var item in DataSource.drones)
            {
                if (item.Id == id)
                {
                    Console.WriteLine(item.ToString());
                    break;
                }
            }

        }

        public void printCustomer(int id)//print the requested Customer
        {
            foreach (var item in DataSource.customers)
            {
                if (item.Id == id)
                {
                    Console.WriteLine(item.ToString());
                    break;
                }
            }

        }

        public void printParcel(int id)//print the requested parcel
        {

            foreach (var item in DataSource.parcels)
            {
                if (item.Id == id)
                {
                    Console.WriteLine(item.ToString());
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
                if (item.Droneld == 0 && item.Id != 0)
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

         // public List<double> R_power_consumption_Drone() 
           
    }
}

