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
        const double speedToSecond = 1000;
        const int sleepTaimer = 500;

        public Simulator(BL bl, int idDrone, Action updateProgress, Func<bool> checkStop)
        {
            DroneToList drone;
            bool stop = false;
            while (!stop) //until we didn't stop the thread
            {
                drone = bl.getDrone(idDrone);

                switch (drone.Status)
                {
                    case DroneStatuses.available:
                        if (bl.relevantParcel_enoughBattary(drone, bl.dal.getParcels()) != null)//if the drone is available - check if there are some parcel that he have enough battery for delivery
                        {
                            bl.assignDroneToParcel(idDrone);
                            updateProgress();

                        }
                        else //if there isn't enough battary for delivery - send the parcel to charging
                        {
                            stop = true;
                            break;
                        }

                        if (drone.Battery == 100)
                        {
                            stop = true;
                            break;
                        }

                        bl.chargingDrone(idDrone);
                        updateProgress();


                        break;
                    case DroneStatuses.maintenance: //if the drone in charging - check if the battary is full and relase drone
                        if (drone.Battery >= 100)
                        {
                            DateTime time = DateTime.Now;
                            bl.freeDroneFromCharging(idDrone, time);
                        }
                        break;
                    case DroneStatuses.delivery: //if the stattus of drone is delivery - check the level and update accordding to level
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
                        break;

                    default:
                        break;

                }

                if (checkStop() == true)
                {
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    Thread.Sleep(sleepTaimer);
                    updateProgress();
                }



            }





        }




    }






}

