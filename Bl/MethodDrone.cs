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
                Battery = DroneToAdd.Battery

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
                        CurrentLocation=item.CurrentLocation,
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

        public void updateNameDrone(int idDrone, string newModel)
        {

        }

        public void updateModelDrone(int idDrone, string newModel)
        {

        }
    }
}
