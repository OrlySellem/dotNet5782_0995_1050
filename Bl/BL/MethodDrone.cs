using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using BO;
using BlApi;

namespace BL
{
   
    partial class BL : IBL
    {
        #region CRUD
        //Create
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addDrone(Drone DroneToAdd, int idStation)
        {
            lock (dal)
            {
                try
                {
                    DO.Drone dalDrone = new DO.Drone()
                    {
                        Id = DroneToAdd.Id,
                        Model = DroneToAdd.Model,
                        MaxWeight = (DO.WeightCategories)DroneToAdd.MaxWeight,
                    };
                    dal.addDrone(dalDrone);


                    DroneToAdd.Battery = rand.Next(20, 40);
                    DroneToAdd.Status = DroneStatuses.maintenance;
                    DO.Station currentStation = dal.getStation(idStation);
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

                        idParcel = 0
                    };
                    drones.Add(DroneToAddBL);
                }
                catch (DO.AlreadyExistException ex)
                {
                    throw new AlreadyExistException(ex.Message);
                }
                catch (DO.chargingException ex)
                {
                    throw new NoFreeChargingStations(ex.Message);
                }

            }
        }

        // Read
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneToList getDrone(int id)
        {
            lock (dal)
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
                            idParcel = item.idParcel,
                        };
                }

                throw new DoesntExistentObjectException("drone");
            }
          
        }


        //Update
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateModelDrone(int idDrone, string newModel)
        {
            lock (dal)
            {
                try
                {
                    DroneToList droneToUpdate_bl = drones.Find(X => X.Id == idDrone);
                    drones.Remove(droneToUpdate_bl);

                    var droneToUpdate_dal = dal.getDrone(idDrone);

                    //Remove the drone from IDAL.DO.drones
                    dal.delFromDrones(droneToUpdate_dal);

                    droneToUpdate_dal.Model = newModel;//update IDAL.DO.drones model
                    droneToUpdate_bl.Model = newModel;//update BlApi.drones model

                    dal.addDrone(droneToUpdate_dal);//To add the update drone to IDAL.DO.drones
                    drones.Add(droneToUpdate_bl);//To add the update drone to BlApi.BO.drones
                }
                catch (DO.DoesntExistentObjectException ex)
                {
                    throw new DoesntExistentObjectException(ex.Message);
                }
                catch (DO.AlreadyExistException ex)
                {
                    throw new AlreadyExistException(ex.Message);
                }

            }

        }

        //Delete
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deleteFromDrones(int IDdroneToDel)
        {
            lock (dal)
            {
                DO.Drone drone = dal.getDrone(IDdroneToDel);
                dal.delFromDrones(drone);
            }          
        }

        #endregion CRUD
        public void chargingDrone(int droneId)
        {
            lock (dal)
            {
                try
                {
                    var droneToUpdate = drones.Find(x => x.Id == droneId);

                    // רק רחפן פנוי ישלח לטעינה
                    if (droneToUpdate.Status == DroneStatuses.available)
                    {
                        double minDistance = 0;
                        //התחנה הקרובה ביותר
                        Station closerStationBL = nearStationToDrone(droneToUpdate, ref minDistance);
                        Location nearStation = closerStationBL.Address;

                        double powerForDistance = available * minDistance;

                        if (powerForDistance > droneToUpdate.Battery)
                        {
                            throw new TheDroneCanNotBeSentForCharging("not enough battery to get charged");
                        }
                        else
                        {
                            //רחפן:
                            drones.Remove(droneToUpdate);
                            droneToUpdate.Battery = droneToUpdate.Battery - powerForDistance;//	מצב סוללה יעודכן בהתאם למרחק בין הרחפן לתחנה
                            droneToUpdate.CurrentLocation = nearStation;//	המיקום ישונה למיקום התחנה
                            droneToUpdate.Status = DroneStatuses.maintenance;//	מצב הרחפן ישונה לתחזוקה
                            drones.Add(droneToUpdate);
                            //תחנה:
                            DO.Station closerStationDal = dal.getStation(closerStationBL.Id);
                            dal.reduceChargeSlots(ref closerStationDal);//הורדת מספר עמדות טעינה פנויות ב1

                            //טעינת רחפן
                            DO.Drone droneToUpdateDAL = dal.getDrone(droneToUpdate.Id);//המרת הרחפן לרחפן של דאל
                            dal.chargingDrone(droneToUpdateDAL, closerStationDal);//הוספת מופע מתאים

                        }
                    }
                }
                catch (DO.DoesntExistentObjectException ex)
                {
                    throw new DoesntExistentObjectException(ex.Message);
                }

                catch (DO.chargingException ex)
                {
                    throw new NoFreeChargingStations(ex.Message);
                }
            }
          
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void freeDroneFromCharging(int idDrone, DateTime newtime)
        {
            lock (dal)
            {
                try
                {
                    DroneToList droneBL = drones.Find(x => x.Id == idDrone);
                    DO.Drone droneDal = dal.getDrone(droneBL.Id);
                    DO.DroneCharge droneChargeDal = dal.getDroneCharge(droneBL.Id);

                    if (droneBL.Status == DroneStatuses.maintenance)
                    {
                        drones.Remove(droneBL);

                        TimeSpan incharging = droneChargeDal.StartedRecharged - newtime;
                        double hoursnInCahrge = incharging.Hours + (((double)(incharging.Minutes)) / 60) + (((double)(incharging.Seconds) / 3600));
                        double batrryCharge = hoursnInCahrge * Drone_charging_speed + droneBL.Battery;

                        if (batrryCharge > 100)
                            batrryCharge = 100;

                        droneBL.Battery = batrryCharge;
                        droneBL.Status = DroneStatuses.available;

                        drones.Add(droneBL);

                        dal.freeDroneCharge(droneDal);
                    }
                    else
                    {
                        throw new OnlyMaintenanceDroneWillBeAbleToBeReleasedFromCharging();
                    }
                }
                catch (DO.DoesntExistentObjectException ex)
                {
                    throw new DoesntExistentObjectException(ex.Message);
                }

            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        internal IEnumerable<DO.Parcel> relevantParcel_enoughBattary(DroneToList drone, IEnumerable<DO.Parcel> parcelsDal)
        {
            lock (dal)
            {
                try
                {
                    double powerForDistance = 0;
                    double distance_DroneToSender, distance_SenderToTarge, distance_TargetToStationt;
                    double totalDistance;
                    List<DO.Parcel> parcels = new List<DO.Parcel>();
                    foreach (var item in parcelsDal)
                    {
                        if (item.Delivered == null)
                        {
                            DO.Customer sender = dal.getCustomer(item.Senderld);
                            DO.Customer target = dal.getCustomer(item.Targetld);
                            double minDistance = 0;
                            Location nearStation = nearStationToCustomer(target.Id, ref minDistance);

                            distance_DroneToSender = Math.Sqrt(Math.Pow(drone.CurrentLocation.Lattitude - sender.Lattitude, 2) + Math.Pow(drone.CurrentLocation.Longitude - sender.Longitude, 2));

                            distance_SenderToTarge = Math.Sqrt(Math.Pow(sender.Lattitude - target.Lattitude, 2) + Math.Pow(sender.Longitude - target.Longitude, 2));

                            distance_TargetToStationt = Math.Sqrt(Math.Pow(target.Lattitude - nearStation.Lattitude, 2) + Math.Pow(target.Longitude - nearStation.Longitude, 2));

                            totalDistance = (distance_DroneToSender + distance_SenderToTarge + distance_TargetToStationt) / Math.Pow(10, 3);


                            if (drone.MaxWeight == WeightCategories.light)
                            {
                                powerForDistance = lightWeight * totalDistance;
                            }

                            if (drone.MaxWeight == WeightCategories.medium)
                            {
                                powerForDistance = mediumWeight * totalDistance;
                            }

                            if (drone.MaxWeight == WeightCategories.heavy)
                            {
                                powerForDistance = heavyWeight * minDistance;
                            }

                            if (powerForDistance <= drone.Battery)
                            {
                                if ((WeightCategories)item.Weight <= drone.MaxWeight)
                                    parcels.Add(item);

                            }
                        }
                    }
                    if (parcels == null)
                    {
                        return null;
                    }

                    return parcels;
                }
                catch (DO.DoesntExistentObjectException ex)
                {

                    throw new DoesntExistentObjectException(ex.Message);
                }

            }

        }

        double distance(DO.Parcel parcelToAssign, DroneToList droneBL)
        {
            double minDistance = Math.Sqrt(Math.Pow(dal.getCustomer(parcelToAssign.Targetld).Lattitude - droneBL.CurrentLocation.Lattitude, 2) + Math.Pow(dal.getCustomer(parcelToAssign.Targetld).Longitude - droneBL.CurrentLocation.Longitude, 2));
            return minDistance / Math.Pow(10, 3);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void assignDroneToParcel(int idDrone)
        {
            lock (dal)
            {
                try
                {
                    DroneToList droneToUpdate = drones.Find(x => x.Id == idDrone);

                    DO.Drone droneDAL = dal.getDrone(idDrone);

                    IEnumerable<DO.Parcel> parcelsDal = dal.getParcels().ToList();
                    IEnumerable<DO.Parcel> parcels = relevantParcel_enoughBattary(droneToUpdate, parcelsDal);

                    if (parcels == null)
                    {
                        throw new NoSuitablePsrcelWasFoundToBelongToTheDrone("There isn't parcel suitable for delivery by the current drone");
                    }

                    DO.Parcel parcelToAssign = parcels.First();

                    double minDistance = 0, distanceItem;

                    if (droneToUpdate.Status == DroneStatuses.available)
                    {
                        foreach (var item in parcels)
                        {
                            if (parcelToAssign.Id == item.Id)
                            {
                                continue;
                            }

                            if (parcelToAssign.Priority < item.Priority)
                            {
                                parcelToAssign = item;
                                minDistance = distance(item, droneToUpdate);
                                continue;
                            }
                            else if (parcelToAssign.Priority == item.Priority)
                            {
                                if (parcelToAssign.Weight < item.Weight)
                                {
                                    if (item.Weight <= droneDAL.MaxWeight)
                                    {
                                        parcelToAssign = item;
                                        minDistance = distance(item, droneToUpdate);
                                        continue;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else if (parcelToAssign.Weight == item.Weight)
                                {
                                    distanceItem = distance(item, droneToUpdate);
                                    if (minDistance > distanceItem)
                                    {
                                        parcelToAssign = item;
                                        minDistance = distance(item, droneToUpdate);
                                        continue;
                                    }
                                    else if (minDistance == distanceItem)
                                    {
                                        if (parcelToAssign.Requested > item.Requested)
                                        {
                                            parcelToAssign = item;
                                            minDistance = distance(item, droneToUpdate);
                                            continue;
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                continue;
                            }

                        }
                        dal.assign_drone_parcel(droneDAL, parcelToAssign);
                        //עדכון תז החבילה שבהעברה
                        drones.Remove(droneToUpdate);
                        droneToUpdate.Status = DroneStatuses.delivery;
                        droneToUpdate.idParcel = parcelToAssign.Id;

                        drones.Add(droneToUpdate);
                    }
                    else
                    {
                        throw new TheDroneCanNotBeSentForCharging("The drone isn't available");
                    }

                }
                catch (DO.DoesntExistentObjectException ex)
                {
                    throw new DoesntExistentObjectException(ex.Message, ex);
                }
                catch (DO.AlreadyExistException ex)
                {
                    throw new AlreadyExistException(ex.Message, ex);
                }
                catch (NoSuitablePsrcelWasFoundToBelongToTheDrone ex)
                {
                    throw new NoSuitablePsrcelWasFoundToBelongToTheDrone(ex.Message, ex);
                };

            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void dronePickParcel(int droneId)
        {
            lock (dal)
            {
                try
                {
                    bool flag = true;
                    var droneToUpdate = drones.Find(x => x.Id == droneId);
                    DO.Parcel parcelItem = dal.getParcel(droneToUpdate.idParcel);

                    if (parcelItem.PickedUp == null && parcelItem.Scheduled != null)
                    {
                        flag = false;
                        drones.Remove(droneToUpdate);

                        var SenderForLocation = dal.getCustomer(parcelItem.Senderld);

                        //	עדכון מצב סוללה לפי המרחק בין מיקום מקורי לבין מיקום השולח
                        double distance = Math.Sqrt(Math.Pow(droneToUpdate.CurrentLocation.Lattitude - SenderForLocation.Lattitude, 2) + Math.Pow(droneToUpdate.CurrentLocation.Longitude - SenderForLocation.Longitude, 2));
                        droneToUpdate.Battery = droneToUpdate.Battery - ((distance / Math.Pow(10, 3)) * available);//אולי אין מספיק בטריה

                        //	עדכון מיקום למיקום השולח
                        droneToUpdate.CurrentLocation = new Location()
                        {
                            Lattitude = SenderForLocation.Lattitude,
                            Longitude = SenderForLocation.Longitude
                        };

                        drones.Add(droneToUpdate);


                        //	 עדכון זמן איסוף חבילה
                        var parcelToUpdate = dal.getParcel(parcelItem.Id);
                        dal.delFromParcels(parcelToUpdate);

                        parcelToUpdate.PickedUp = DateTime.Now;
                        dal.addParcel(parcelToUpdate);
                    }
                    else
                        throw new DelivereyAlreadyArrive("The drone has already picked up the parcel");

                    if (flag)
                        throw new DeliveryCannotBeMade("The delivery Cannot Be Made");

                }
                catch (DO.DoesntExistentObjectException ex)
                {
                    throw new DoesntExistentObjectException(ex.Message);
                }
                catch (DO.AlreadyExistException ex)
                {
                    throw new AlreadyExistException(ex.Message);
                }
            }
            
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deliveryArivveToCustomer(int droneId)
        {
            lock (dal)
            {
                try
                {
                    var droneToUpdate = drones.Find(x => x.Id == droneId);
                    var parcel_Ascribed_drone = dal.getParcel(droneToUpdate.idParcel);

                    if (parcel_Ascribed_drone.PickedUp != null && parcel_Ascribed_drone.Delivered == null)
                    {
                        //רחפן
                        //	עדכון מצב סוללה לפי המרחק בין מיקום מקורי לבין מיקום יעד המשלוח
                        var TargetldForLocation = dal.getCustomer(parcel_Ascribed_drone.Targetld);
                        double distance = Math.Sqrt(Math.Pow(droneToUpdate.CurrentLocation.Lattitude - TargetldForLocation.Lattitude, 2) + Math.Pow(droneToUpdate.CurrentLocation.Longitude - TargetldForLocation.Longitude, 2));

                        if (droneToUpdate.MaxWeight == WeightCategories.light)
                            droneToUpdate.Battery = droneToUpdate.Battery - (distance / Math.Pow(10, 3) * lightWeight);//אולי אין מספיק בטריה
                        if (droneToUpdate.MaxWeight == WeightCategories.medium)
                            droneToUpdate.Battery = droneToUpdate.Battery - (distance / Math.Pow(10, 3) * mediumWeight);//אולי אין מספיק בטריה
                        if (droneToUpdate.MaxWeight == WeightCategories.heavy)
                            droneToUpdate.Battery = droneToUpdate.Battery - (distance / Math.Pow(10, 3) * available);//אולי אין מספיק בטריה

                        //	עדכון מיקום למיקום יעד המשלוח
                        droneToUpdate.CurrentLocation = new Location()
                        {
                            Lattitude = TargetldForLocation.Lattitude,
                            Longitude = TargetldForLocation.Longitude
                        };

                        //	שינוי מצב רחפן לפנוי
                        droneToUpdate.Status = DroneStatuses.available;


                        //	עדכון זמן אספקה
                        dal.delivery_arrive_toCustomer(parcel_Ascribed_drone);
                    }
                    else
                        throw new DroneCantBeAssigend("The drone can't be assign");
                }

                catch (DO.DoesntExistentObjectException ex)
                {
                    throw new DoesntExistentObjectException(ex.Message);
                }

            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> GetDrones(Predicate<DroneToList> predicate = null)
        {
            lock (dal)
            {
                return drones.FindAll(x => predicate == null ? true : predicate(x));
            }
        }

    }


}
