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
    public class DalObject : IDAL.DO.IDal
    {
        internal static Random rand = new Random();

        public DalObject()
        {
            DataSource.Initialize();
        }
        #region ADD
        public void addStaion(Station stationToAdd) //דוגמא בויה לסידור +ה find
        { 

            int indexStation = findIndexStation(stationToAdd.Id);
            if (indexStation == -1)
                throw new stationException("already exist");

            DataSource.stations.Add(stationToAdd);
        }
        public void addDrone(Drone droneToAdd)
        {
            int indexDrone = findIndexDrone(droneToAdd.Id);
            if (indexDrone != -1)
                throw new droneException("already exist");

            DataSource.drones.Add(droneToAdd);
        }

        public void addCustomer(Customer CustomerToAdd)
        {
            int indexCustomer = findIndexCustomer(CustomerToAdd.Id);
            if (indexCustomer != -1)
                throw new customerException("already exist");

            DataSource.customers.Add(CustomerToAdd);
        }

        public void addParcel(Parcel ParcelToAdd)
        {
            DataSource.parcels.Add(ParcelToAdd);
        }
        #endregion

        #region find
        public int findIndexParcel(int id)//Finds the requested parcel from the arr
        {
            for (int i = 0; i < DataSource.parcels.Count; i++)
            {
                if (DataSource.parcels[i].Id == id)
                {
                    return i;//return the index
                }
            }
            throw new parcelException("isn't exist");//if the id isn't esixt in the parcel's array
        }

        public Drone findIndexDrone(int id)//Finds the requested drone from the arr
        {
            IEnumerable< Drone> e = DataSource.drones;

            var drone = DataSource.drones.Where(d => d.Id == id);
            if (drone == null)
                throw new stationException("isn't exist");
            return drone;

        }

        //מעודכן להראות לאורלי
        public int findIndexStation(int id)//Finds the requested station from the arr
        {
            for (int i = 0; i < DataSource.drones.Count; i++)
            {
                if (DataSource.drones[i].Id == id)
                    return i;
            }

            throw new droneException("isn't exist");

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

            throw new customerException("isn't exist");
        }
        #endregion
        public void reduceChargeSlots(int indexStation)
        {
            Station s = DataSource.stations[indexStation];
            s.ChargeSlots--;//Reduce the number of claim positions
            DataSource.stations[indexStation] = s;
        }

        public void plusChargeSlots (int indexStation)
        {
            Station st = DataSource.stations[indexStation];
            st.ChargeSlots++;
            DataSource.stations[indexStation] = st;
        }

        public void assign_drone_parcel_PA (int indexDrone, int indexParcel)//assign parcel to drone
        {
                DataSource.parcels[indexParcel].Droneld = DataSource.drones[indexDrone].Id;
                DataSource.parcels[indexParcel].Scheduled = DateTime.Now;      
        }

        public void assign_drone_parcel_DR (int idDrone, int idParcel)//assign parcel to drone
        {
            int indexDrone = findIndexDrone(idDrone);
            int indexParcel = findIndexParcel(idParcel);

           if (DataSource.drones[indexDrone].Status == DroneStatuses.available)
            {
                assign_drone_parcel_PA(indexDrone, indexParcel);
                DataSource.drones[indexDrone].Status = DroneStatuses.delivery;
            }
            else
            {
                throw new droneException("isn't available");
            }
        }

        public void drone_pick_parcel(int idDrone, int idParcel)//pick up parcel by drone
        {
            int indexDrone=findIndexDrone(idDrone);
            int indexParcel = findIndexParcel(idParcel);

            Parcel p = DataSource.parcels[indexParcel];
            p.PickedUp = DateTime.Now;
            DataSource.parcels[indexParcel] = p;

        }


        public void delivery_arrive_toCustomer(int idDrone, int idParcel)//The delivery arrived to the customer
        {
            int indexDrone = findIndexDrone(idDrone);
            int indexParcel = findIndexParcel(idParcel);

            Parcel pd = DataSource.parcels[indexParcel];
            pd.Delivered = DateTime.Now;
            DataSource.parcels[indexParcel] = pd;

        }

        public void chargingDrone(int idDrone, int idStation)//Inserts a drone to charg
        {

            int indexDrone = findIndexDrone(idDrone);
            int indexStation = findIndexStation(idStation);

            if (DataSource.stations[indexStation].ChargeSlots == 0)//If there are no charge slots available, choose a new station  
            {
                throw new stationException("isn't available");
            }
            else
            {
                 reduceChargeSlots(indexStation);
            }

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
          
            plusChargeSlots(indexStation);
        }

 
     
      public double [] R_power_consumption_Drone()
        {
            double [] power = new double [5];
            power[0]=DataSource.Config.available;
            power[1]=DataSource.Config.Lightweight;
            power[2]=DataSource.Config.MediumWeight;
            power[3]=DataSource.Config.Heavyweight;
            power[4]=DataSource.Config.Drone_charging_speed;

            return power;
        }

       

      

        public void assign_parcel_drone()
        {
            throw new NotImplementedException();
        }

        public void drone_pick_parcel()
        {
            throw new NotImplementedException();
        }

        public void delivery_arrive_toCustomer()
        {
            throw new NotImplementedException();
        }

        public void chargingDrone()
        {
            throw new NotImplementedException();
        }

        public void freeDroneCharge()
        {
            throw new NotImplementedException();
        }
    }






}

