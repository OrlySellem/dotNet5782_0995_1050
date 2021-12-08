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

                  numParcel =0
        };

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

        public IEnumerable <DroneToList> getAllDronens()
        {
            return drones;      
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
            catch(IDAL.DO.DoesntExistException ex)
            {
                throw new UpdateProblemException("The drone doesn't exist in the system",ex);
            }
            catch (IDAL.DO.AlreadyExistException ex)
            {
                throw new AddingProblemException("The drone already exist",ex);
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



        public void freeDroneCharge(int id, float chargingTime)
        {
           if (drones.find
        }



        public void dronePickParcel(int droneId)
        {
            bool flag = true;
            IEnumerable<IDAL.DO.Parcel> parcelsFromDS = dal.getAllParcels();

            foreach (var parcelItem in parcelsFromDS)
            {
                if (parcelItem.Droneld== droneId)
                {
                    if( parcelItem.PickedUp== new DateTime(01, 01, 0001))
                    {
                        flag = false;
                        var droneToUpdate = drones.Find(x => x.Id == droneId);
                        drones.Remove(droneToUpdate);

                       var SenderForLocation=  dal.getCustomer(parcelItem.Senderld);

                        //	עדכון מצב סוללה לפי המרחק בין מיקום מקורי לבין מיקום השולח
                        double distance = Math.Sqrt(Math.Pow(droneToUpdate.CurrentLocation.Lattitude - SenderForLocation.Lattitude, 2) + Math.Pow(droneToUpdate.CurrentLocation.Longitude - SenderForLocation.Longitude, 2));
                        droneToUpdate.Battery = droneToUpdate.Battery -( distance * available);//אולי אין מספיק בטריה

                        //	עדכון מיקום למיקום השולח
                        droneToUpdate.CurrentLocation = new Location()
                        {
                            Lattitude = SenderForLocation.Lattitude,
                            Longitude = SenderForLocation.Longitude
                        };
                        drones.Add(droneToUpdate);



                    }
                   
                    throw new UpdateProblemException("The drone has already picked up the parcel.");

                }
            }
            if (flag)
                throw new UpdateProblemException("The drone doesn't make a delivery");




        }

    }
}
