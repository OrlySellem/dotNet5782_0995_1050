using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public enum WeightCategories { light, medium, heavy }

    public enum Priorities { normal, fast, emergency }

    public enum programDelivry { addingOptions, UpdateOptions, DisplayOptions, DisplayListOptions, exit }

    public enum Add { addCustomer, addDrone, addStation, addParcel }

    public enum Update { assignParcelDrone, dronePickParcel, deliveryAriveToCustomer, chargingDrone, freeDroneCharge }

    public enum Display { displayCustomer, displayDrone, displayStation, displayParcel }

    public enum DisplayListOptions { displayCustomers, displayDrones, displayStations, displayParcels, display_parcels_without_drone, display_station_with_freeChargingStations }

}