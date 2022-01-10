using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;
using static BL.BL;

namespace BL
{
    internal class Simulator
    {
        const double speedToSecond = 1;

        Stopwatch stopwatch;

        public Simulator (BL bl, int idDrone, Action update, Func<bool> checkStop)
        {
            while (checkStop()) //until we didn't stop the thread
            {
                lock (bl) 
                {
                    var drone = bl.getDrone(idDrone);

                    if (drone.Status == DroneStatuses.maintenance) //if the drone in charging - check if the battary is full and relase drone
                    {
                        if (drone.Battery >= 100)
                        {
                            DateTime time =  DateTime.Now;
                            bl.freeDroneFromCharging(idDrone, time);                            
                        }
                    }
                    
                    if (drone.Status == DroneStatuses.available) //if the drone is available - check if there are some parcel that he have enough battery for delivery
                    {
                        if (bl.relevantParcel_enoughBattary(drone, bl.dal.getParcels()) != null)
                        {
                            bl.assignDroneToParcel(idDrone);
                        }
                        else //if there isn't enough battary for delivery - send the parcel to charging
                        {
                            bl.chargingDrone(idDrone);
                        }
                    }
                        
                    if (drone.Status == DroneStatuses.delivery) //if the stattus of drone is delivery - check the level and update accordding to level
                    {
                        //find the parcel that assigned to drone
                        var parcel = (from p in bl.getAllParcels()
                                      where p.Id == drone.idParcel
                                      select p).FirstOrDefault();
                        if (parcel.ParcelStatus == ParcelStatus.scheduled)
                        {
                            bl.dronePickParcel(idDrone);
                        }

                        if (parcel.ParcelStatus == ParcelStatus.PickedUp)
                        {
                            bl.deliveryArivveToCustomer(idDrone);
                        }
                    }
                }
                

            }





        }




    }






}
}
