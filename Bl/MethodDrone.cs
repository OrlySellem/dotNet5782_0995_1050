using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public partial class BL: IBL
    {
        public void addDrone(Drone DroneToAdd, int idStation)
        {
            

            IDAL.DO.Drone dalDrone = new IDAL.DO.Drone()
            {
                Id = DroneToAdd.Id,
                Model = DroneToAdd.Model,
                MaxWeight = (IDAL.DO.WeightCategories)DroneToAdd.MaxWeight,
                Battery = DroneToAdd.Battery   
                
            };

            dal.addDrone(dalDrone);

            IDAL.DO.Station currentStation = dal.getStation(idStation);
            dal.chargingDrone(dalDrone, currentStation);
           

        }

        public void updateModelDrone(int idDrone, string newModel)
        {
           
            

        }
    }
}
