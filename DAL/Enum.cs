/* Ester Shmuel ID:318968468 && Orly Sellem ID:315208728
  Lecturer: Adina Milston
  Course: .Dot Net
  Exercise: 2
  The purpose of this file is to menenge all the enum
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public enum WeightCategories { light, medium, heavy }

        public enum Priorities { normal, fast, emergency }

        public enum programDelivry { addingOptions, UpdateOptions, DisplayOptions, DisplayListOptions,  exit }

        public enum Add { addCustomer, addDrone, addStation, addParcel }

        public enum Update { assignParcelDrone, dronePickParcel, deliveryAriveToCustomer, chargingDrone, freeDroneCharge }

        public enum Display { displayCustomer, displayDrone, displayStation, displayParcel }

        public enum DisplayListOptions { displayCustomers, displayDrones, displayStations, displayParcels, display_parcels_without_drone, display_station_with_freeChargingStations }

    }
}
