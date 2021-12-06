using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public partial class BL : IBL
    {
        public void addDrone(Drone DroneToAdd, int idStation)
        {

            IDAL.DO.Drone dalDrone = new IDAL.DO.Drone()
            {
                Id = DroneToAdd.Id,
                Model = DroneToAdd.Model,
                MaxWeight = (IDAL.DO.WeightCategories)DroneToAdd.MaxWeight,
            };

            dal.addDrone(dalDrone);

            IDAL.DO.Station currentStation = dal.getStation(idStation);
            dal.chargingDrone(dalDrone, currentStation);


        }

        public DroneToList getDrone(int id)
        {

            foreach (BO.DroneToList item in drones)
            {

                if (item.Id == id)
                    return new DroneToList()
                    {
                        Id = item.Id,
                        Model = item.Model,
                        MaxWeight = item.MaxWeight,
                        Battery = item.Battery,
                        Status = item.Status,
                        CurrentLocation = item.CurrentLocation,
                        numParcel = item.numParcel,
                    };
            }

            throw new droneException(" isn't exist");

        }

        public IEnumerable<StationToList> getAllDronens()
        {


        }

        /*public void getDrone()
        {
            double[] drones = dal.R_power_consumption_Drone();

        }*/

        public void updateModelDrone(int idDrone, string newModel)
        {
            var droneToUpdate = dal.getDrone(idDrone);

            //Remove the drone from IDAL.DO.drones
            dal.delFromDrones(droneToUpdate);

            //Remove the drone from BL.drones
            var myDrone = getDrone(idDrone);
            drones.Remove(myDrone);

            droneToUpdate.Model = newModel;//update IDAL.DO.drones model
            myDrone.Model = newModel;//update BL.drones model

            dal.addDrone(droneToUpdate);//To add the update drone to IDAL.DO.drones

            drones.Add(myDrone);//To add the update drone to IBL.BO.drones
        }

        public void chargingDrone(int droneId)
        {
            var droneToUpdate = drones.Find(x => x.Id == droneId);

            // רק רחפן פנוי ישלח לטעינה
            if (droneToUpdate.Status == DroneStatuses.available)
            {
                double minDistance = 0; 
                //התחנה הקרובה ביותר
                Location nearStation = nearStationToCustomer(droneId, ref minDistance);

                double powerForDistance = available * minDistance;
                //droneToUpdate.Status = DroneStatuses.delivery;
                if (powerForDistance > droneToUpdate.Battery)
                {
                    throw new  droneException ("not enough battery to get charged");
                }
                else
                {
                    Station closerStationBL = nearStationToDrone(droneToUpdate.Id);
                    IDAL.DO.Station closerStationDal = dal.getStation(closerStationBL.Id);
                    
                    
                    dal.reduceChargeSlots(ref closerStationDal);//הורדת מספר עמדות טעינה פנויות ב1
                    IDAL.DO.Drone droneToUpdateDAL = dal.getDrone(droneToUpdate.Id);//המרת הרחפן לרחפן של דאל
                    dal.chargingDrone(droneToUpdateDAL, closerStationDal);//הוספת מופע מתאים

                    drones.Remove(droneToUpdate);
                    //מצב סוללה יעודכן בהתאם למרחק - לעשות
                    droneToUpdate.CurrentLocation = closerStationBL.Address;
                    droneToUpdate.Status = DroneStatuses.maintenance;
                    drones.Add(droneToUpdate);

                   
                }
            }

        }

    }
}
