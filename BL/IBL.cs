using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
namespace IBL
{
    public interface IBL
    {
        #region ADD
        public void addStation(Station StationToAdd, int idStation);

        public void addDrone(Drone DroneToAdd);// add drone

        public void addCustomer(Customer CustomerToAdd);// add customer

        public void addParcel(Parcel ParcelToAdd);//add new base percel
        #endregion

        void updateModelDrone(int idDrone, string newModel);
        

    }
}
