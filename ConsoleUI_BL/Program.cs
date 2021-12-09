using System;
using System.Collections.Generic;
using IBL.BO;

namespace ConsoleUI_BL
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                IBL.BL mainBl = new IBL.BL();
                bool flag = true;

                //Variable of station
                int id, name_int, chargeSlots;
                double longitude, lattitude;
                string name_st, phone;

                while (flag)
                {

                    int n = int.Parse(Console.ReadLine());
                    programDelivry outsideChoice = (programDelivry)n;

                    switch (outsideChoice)
                    {
                        #region addChoice

                        case programDelivry.addingOptions:
                            Console.WriteLine("To add station - enter 0\nTo add drone - enter 1\nTo add customer - enter 2\nTo add parcel - enter 3\n");
                            int k = int.Parse(Console.ReadLine());
                            Console.WriteLine();
                            Add addChoice = (Add)k;

                            switch (addChoice)
                            {
                                case Add.addStation:

                                    Console.WriteLine("Please enter station's id:");
                                    id = int.Parse(Console.ReadLine());

                                    Console.WriteLine("Please enter station's name:");
                                    name_int = int.Parse(Console.ReadLine());

                                    Console.WriteLine("Please enter the station's longitude");
                                    longitude = double.Parse(Console.ReadLine());

                                    Console.WriteLine("Please enter the station's lattitude");
                                    lattitude = double.Parse(Console.ReadLine());

                                    Console.WriteLine("Please enter the station's chargeSlots");
                                    chargeSlots = int.Parse(Console.ReadLine());

                                    Location stationLocation = new Location()
                                    {
                                        Longitude = longitude,
                                        Lattitude = lattitude
                                    };

                                    Station newStation = new Station()
                                    {
                                        Id = id,
                                        Name = name_int,
                                        Address = stationLocation,
                                        ChargeSlots = chargeSlots,
                                        Charging_drones = null
                                    };


                                    mainBl.addStation(newStation);

                                    break;
                                case Add.addDrone:

                                    //Ask the user to insert the drone's details
                                    Console.WriteLine("Please enter drone's id:");
                                    id = int.Parse(Console.ReadLine());

                                    Console.WriteLine("Please enter drone's model:");
                                    string model = Console.ReadLine();

                                    Console.WriteLine("Please enter drone's weight categories - 0 for light, 1 for medium, 2 for heavy:");
                                    int maxWeight = int.Parse(Console.ReadLine());

                                    Console.WriteLine("Please enter station's ID to charg the drone:");
                                    int idStation = int.Parse(Console.ReadLine());

                                    Drone droneToAdd = new Drone()
                                    {
                                        Id = id,
                                        Model = model,
                                        MaxWeight = (WeightCategories)maxWeight,

                                    };
                                    mainBl.addDrone(droneToAdd, idStation);

                                    break;

                                case Add.addCustomer:

                                    Console.WriteLine("Please enter your id:");
                                    id = int.Parse(Console.ReadLine());

                                    Console.WriteLine("Please enter your name:");
                                    name_st = Console.ReadLine();

                                    Console.WriteLine("Please enter your phone:");
                                    phone = Console.ReadLine();

                                    Console.WriteLine("Please enter lattitude:");
                                    lattitude = double.Parse(Console.ReadLine());

                                    Console.WriteLine("Please enter longitude:");
                                    longitude = double.Parse(Console.ReadLine());

                                    Location customerLocation = new Location()
                                    {
                                        Longitude = longitude,
                                        Lattitude = lattitude
                                    };

                                    Customer customerToAdd = new Customer()
                                    {
                                        Id = id,
                                        Name = name_st,
                                        Phone = phone,
                                        Address = customerLocation
                                    };
                                    mainBl.addCustomer(customerToAdd);
                                    break;

                                case Add.addParcel:
                                    Console.WriteLine("Please enter sender's id:");
                                    int senderld = int.Parse(Console.ReadLine());

                                    Console.WriteLine("Please enter target's id:");
                                    int targetld = int.Parse(Console.ReadLine());

                                    Console.WriteLine("Please enter drone's weight categories - 0 for light, 1 for medium, 2 for heavy:");
                                    int weight = int.Parse(Console.ReadLine());

                                    Console.WriteLine("Please enter the delivery's priority - 0 for normal, 1 for fast, 2 for emergency:");
                                    int priority = int.Parse(Console.ReadLine());
                                    Console.WriteLine();

                                    Parcel parcelToAdd = new Parcel()
                                    {
                                        Senderld = senderld,
                                        Targetld = targetld,
                                        Weight = (WeightCategories)weight,
                                        Priority = (Priorities)priority,
                                    };
                                    mainBl.addParcel(parcelToAdd);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        #endregion

                        #region updateChoice
                        case programDelivry.UpdateOptions:

                            Console.WriteLine("To assign parcel to drone - enter 0\nTo pick up parcel by drone - enter 1\nTo update that delivery has arrived - enter 2\nTo send drone to charge in base station - enter 3\nTo free drone from chraging - enter 4\n");
                            int m = int.Parse(Console.ReadLine());
                            Update updateChoice = (Update)m;

                            string modelDrone;

                            switch (updateChoice)
                            {
                                case Update.updateDrone:

                                    Console.WriteLine("Please enter drone's id:");
                                    id = int.Parse(Console.ReadLine());

                                    Console.WriteLine("Please enter drone's model:");
                                    modelDrone = Console.ReadLine();

                                    mainBl.updateModelDrone(id, modelDrone);
                                    break;

                                case Update.updateStation:
                                    Console.WriteLine("Please enter station's id:");
                                    id = int.Parse(Console.ReadLine());

                                    Console.WriteLine("Please enter station's name:");
                                    name_int = int.Parse(Console.ReadLine());

                                    Console.WriteLine("Please enter the station's chargeSlots");
                                    chargeSlots = int.Parse(Console.ReadLine());

                                    mainBl.updateStation(id, name_int, chargeSlots);

                                    break;

                                case Update.updateCustomer:

                                    Console.WriteLine("Please enter your id:");
                                    id = int.Parse(Console.ReadLine());

                                    Console.WriteLine("Please enter your name:");
                                    name_st = Console.ReadLine();

                                    Console.WriteLine("Please enter your phone:");
                                    phone = Console.ReadLine();

                                    mainBl.updateCustomer(id, name_st, phone);
                                    break;

                                case Update.chargingDrone:
                                    Console.WriteLine("Please enter drone's id:");
                                    id = int.Parse(Console.ReadLine());
                                    mainBl.chargingDrone(id);
                                    break;

                                case Update.freeDroneCharge:
                                    Console.WriteLine("Please enter drone's id:");
                                    id = int.Parse(Console.ReadLine());

                                    Console.WriteLine("How long has the drone been charging");
                                    TimeSpan chargingTime = TimeSpan.Parse(Console.ReadLine());

                                    mainBl.freeDroneFromCharging(id, chargingTime);
                                    break;

                                case Update.assignParcelDrone:
                                    Console.WriteLine("Please enter drone's id:");
                                    id = int.Parse(Console.ReadLine());

                                    mainBl.assignDroneToParcel(id);

                                    break;
                                case Update.dronePickParcel:
                                    Console.WriteLine("Please enter drone's id:");
                                    id = int.Parse(Console.ReadLine());

                                    mainBl.dronePickParcel(id);
                                    break;

                                case Update.deliveryAriveToCustomer:

                                    Console.WriteLine("Please enter drone's id:");
                                    id = int.Parse(Console.ReadLine());

                                    mainBl.deliveryAriveToCustomer(id);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        #endregion 

                        #region displayEntity
                        case programDelivry.DisplayOptions:
                            Console.WriteLine("To display stations - enter 0\nTo display drones - enter 1\nTo display stations - enter 2\nTo display parcels - enter 3\nTo displays a list of parcels without assign to drones - enter 4\nTo display base stations with available charging drones - enter 5\n");
                            int t = int.Parse(Console.ReadLine());
                            Display DisplayChoice = (Display)t;


                            switch (DisplayChoice)
                            {
                                case Display.displayStation:

                                    Console.WriteLine("Please enter station's id:");
                                    id = int.Parse(Console.ReadLine());

                                    IBL.BO.Station stationToPrint = mainBl.getStation(id);
                                    stationToPrint.ToString();
                                    break;

                                case Display.displayDrone:
                                    Console.WriteLine("Please enter drone's id:");
                                    id = int.Parse(Console.ReadLine());

                                    IBL.BO.DroneToList droneToPrint = mainBl.getDrone(id);
                                    droneToPrint.ToString();
                                    break;

                                case Display.displayCustomer:
                                    Console.WriteLine("Please enter customer's id:");
                                    id = int.Parse(Console.ReadLine());

                                    IBL.BO.Customer customerToPrint = mainBl.getCustomer(id);
                                    customerToPrint.ToString();
                                    break;

                                case Display.displayParcel:
                                    Console.WriteLine("Please enter parcel's id:");
                                    id = int.Parse(Console.ReadLine());

                                    IBL.BO.Parcel parcelToPrint = mainBl.getParcel(id);
                                    parcelToPrint.ToString();
                                    break;
                            }


                            break;
                        #endregion displayEntity

                        #region displayListOptions
                        case programDelivry.DisplayListOptions:
                            Console.WriteLine("To display stations - enter 0\nTo display drones - enter 1\nTo display customers - enter 2\nTo display parcels - enter 3\nTo displays a list of parcels without assign to drones - enter 4\nTo display base stations with available charging drones - enter 5\n");
                            t = int.Parse(Console.ReadLine());
                            DisplayListOptions Display_list_choice = (DisplayListOptions)t;
                            switch (Display_list_choice)
                            {
                                case DisplayListOptions.displayStations:

                                    List<StationToList> stations = (List<StationToList>)mainBl.getAllStations();
                                    foreach (StationToList item in stations)
                                    {
                                        item.ToString();
                                    }
                                    break;

                                case DisplayListOptions.displayDrones:

                                    List<DroneToList> drones = (List<DroneToList>)mainBl.getAllDronens();
                                    foreach (DroneToList item in drones)
                                    {
                                        item.ToString();
                                    }
                                    break;

                                case DisplayListOptions.displayCustomers:

                                    List<CustomerToList> customers = (List<CustomerToList>)mainBl.getAllCustomers();
                                    foreach (CustomerToList item in customers)
                                    {
                                        item.ToString();
                                    }
                                    break;

                                case DisplayListOptions.displayParcels:

                                    List<ParcelToList> parcels = (List<ParcelToList>)mainBl.getAllParcels();
                                    foreach (ParcelToList item in parcels)
                                    {
                                        item.ToString();
                                    }
                                    break;

                                //לממש
                                case DisplayListOptions.display_parcels_without_drone:
                                    List<ParcelToList> parcels_without_drone = (List<ParcelToList>)mainBl.ParcelDoesntAssignToDrone();
                                    foreach (ParcelToList item in parcels_without_drone)
                                    {
                                        item.ToString();
                                    }
                                    break;

                                case DisplayListOptions.display_station_with_freeChargingStations:
                                    List<StationToList> stations_with_freeChargingStation = (List<StationToList>)mainBl.display_station_with_freeChargingStations();
                                    foreach (StationToList item in stations_with_freeChargingStation)
                                    {
                                        item.ToString();
                                    }

                                    break;


                            }
                            break;
                        #endregion displayListOptions


                        case programDelivry.exit:
                            break;
                        default:
                            break;
                    }
                }
            }

            catch (IBL.BO.AlreadyExistException ex)
            {
                Console.WriteLine(ex.ToString()); 
            }
            catch (IBL.BO.UpdateProblemException ex)
            {
                Console.WriteLine(e);
            }
            catch (IBL.BO.GetDetailsProblemException ex)
            {
                Console.WriteLine(ex);
            }
            }

        }
    }
}
