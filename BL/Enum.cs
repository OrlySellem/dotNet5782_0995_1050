using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public enum WeightCategories { light, medium, heavy }

        public enum Priorities { normal, fast, emergency }

        public enum programDelivry { addingOptions, UpdateOptions, DisplayOptions, DisplayListOptions,  exit }

        public enum Add { addStation, addDrone, addCustomer, addParcel }

        public enum Update { updateDrone, updateStation, updateCustomer, chargingDrone, freeDroneCharge, assignParcelDrone, dronePickParcel, deliveryAriveToCustomer }

        public enum Display { displayStation, displayDrone, displayCustomer, displayParcel }

        public enum DisplayListOptions { displayCustomers, displayDrones, displayStations, displayParcels, display_parcels_without_drone, display_station_with_freeChargingStations }

        public enum DroneStatuses { available, maintenance, delivery }

        public enum ParcelStatus { requested, scheduled, PickedUp, Delivered }

    }
        
}
