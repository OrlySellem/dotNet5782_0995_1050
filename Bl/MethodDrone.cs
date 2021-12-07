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
            try
            {
                IDAL.DO.Drone dalDrone = new IDAL.DO.Drone()
                {
                    Id = DroneToAdd.Id,
                    Model = DroneToAdd.Model,
                    MaxWeight = (IDAL.DO.WeightCategories)DroneToAdd.MaxWeight,
                };
                dal.addDrone(dalDrone);


                DroneToAdd.Battery = rand.Next(20, 40);
                DroneToAdd.Status = DroneStatuses.maintenance;
                IDAL.DO.Station currentStation = dal.getStation(idStation);
                DroneToAdd.CurrentLocation = new Location()
                {
                    Lattitude = currentStation.Lattitude,
                    Longitude = currentStation.Longitude
                };
                dal.chargingDrone(dalDrone, currentStation);

                DroneToList DroneToAddBL = new DroneToList()
                {
                    Id = DroneToAdd.Id,

                    Model = DroneToAdd.Model,

                    MaxWeight = DroneToAdd.MaxWeight,

                    Battery = DroneToAdd.Battery,

                  Status = DroneToAdd.Status,

                  CurrentLocation = DroneToAdd.CurrentLocation,

                  numParcel =
                };
                IDAL.DO.Station currentStation = dal.getStation(idStation);
                dal.chargingDrone(dalDrone, currentStation);

            }
            catch (IDAL.DO.AlreadyExistException ex)
            {
                throw new AddingProblemException("The parcel already exist", ex);
            }


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

            throw new GetDetailsProblemException("The drone doesn't exist in the system");
        }

        public IEnumerable<StationToList> getAllDronens()
        {


        }

        public void updateModelDrone(int idDrone, string newModel)
        {
            try
            {
                var droneToUpdate_dal = dal.getDrone(idDrone);

                //Remove the drone from IDAL.DO.drones
                dal.delFromDrones(droneToUpdate_dal);

                //Remove the drone from BL.drones
                var droneToUpdate_bl = getDrone(idDrone);
                drones.Remove(droneToUpdate_bl);

                droneToUpdate_dal.Model = newModel;//update IDAL.DO.drones model
                droneToUpdate_bl.Model = newModel;//update BL.drones model

                dal.addDrone(droneToUpdate_dal);//To add the update drone to IDAL.DO.drones
                drones.Add(droneToUpdate_bl);//To add the update drone to IBL.BO.drones
            }
            catch (IDAL.DO.DoesntExistException ex)
            {
                throw new UpdateProblemException("The drone doesn't exist in the system", ex);
            }
            catch (IDAL.DO.AlreadyExistException ex)
            {
                throw new AddingProblemException("The drone already exist", ex);
            }

        }

        public void chargingDrone(int droneId)
        {
            try
            {
                var droneToUpdate = drones.Find(x => x.Id == droneId);

                // רק רחפן פנוי ישלח לטעינה
                if (droneToUpdate.Status == DroneStatuses.available)
                {
                    double minDistance = 0;
                    //התחנה הקרובה ביותר
                    Station closerStationBL = nearStationToDrone(droneToUpdate.Id, ref minDistance);
                    Location nearStation = closerStationBL.Address;

                    double powerForDistance = available * minDistance;

                    if (powerForDistance > droneToUpdate.Battery)
                    {
                        throw new UpdateProblemException("not enough battery to get charged");
                    }
                    else
                    {
                        //רחפן:
                        drones.Remove(droneToUpdate);
                        droneToUpdate.Battery = droneToUpdate.Battery-powerForDistance;//	מצב סוללה יעודכן בהתאם למרחק בין הרחפן לתחנה
                        droneToUpdate.CurrentLocation = nearStation;//	המיקום ישונה למיקום התחנה
                        droneToUpdate.Status = DroneStatuses.maintenance;//	מצב הרחפן ישונה לתחזוקה
                        drones.Add(droneToUpdate);
                        //תחנה:
                        IDAL.DO.Station closerStationDal = dal.getStation(closerStationBL.Id);
                        dal.reduceChargeSlots(ref closerStationDal);//הורדת מספר עמדות טעינה פנויות ב1

                        //טעינת רחפן
                        IDAL.DO.Drone droneToUpdateDAL = dal.getDrone(droneToUpdate.Id);//המרת הרחפן לרחפן של דאל
                        dal.chargingDrone(droneToUpdateDAL, closerStationDal);//הוספת מופע מתאים

                    }
                }
            }
            catch (IDAL.DO.DoesntExistException ex)
            {
                throw new UpdateProblemException(ex);
            }
        }


        public void freeDroneFromCharging(int idDrone, TimeSpan time)
        {
            DroneToList a = drones.Find(x => x.Id == idDrone);

            if (a.Status == DroneStatuses.maintenance)
            {
                a.Battery = time.Hours * Drone_charging_speed;
                a.Status = DroneStatuses.available;

            }
            else
            {
                throw new GetDetailsProblemException("The drone doesn't in maintenance");
            }

        }

        public void assignDroneToParcel(int idDrone)
        {
            try
            {
                DroneToList d = drones.Find(x => x.Id == idDrone);
                IDAL.DO.Drone droneDAL = dal.getDrone(idDrone);
                dal.delFromDrones(droneDAL);
                IEnumerable<IDAL.DO.Parcel> parcels = dal.getAllParcels();
                if (d.Status == DroneStatuses.available)
                {
                   if()






                }

            }
            catch (Exception)
            {

                throw;
            }
            


        }

        public void
    }
}
