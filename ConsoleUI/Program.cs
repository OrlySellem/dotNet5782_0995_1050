using System;
using IDAL.DO;
using DAL;
using DalObject;

namespace ConsoleUI
{
    enum programDelivry { addingOptions, UpdateOptions, DisplayOptions, DisplayListOptions, exit }
    enum Add { addCustomer, addDrone, addStation, addParcel }
    enum Update { assignParcelDrone, dronePickParcel, deliveryAriveToCustomer, chargingDrone, freeDroneCharge}
    enum Display { displayCustomer, displayDrone, displayStation, displayParcel }
    enum DisplayListOptions { displayCustomers, displayDrones, displayStations, displayParcels, display_parcels_without_drone, display_station_with_freeChargingStations }

    class Program
    {
        static void Main(string[] args)
        {
            bool flag = true;
            while (flag)
            {
                Console.Write("To adding customer, drone, station or parcel - enter 0\nTo update items - enter 1\nTo display options - enter 2\nTo display list's options - enter 3\nTo exit - enter 4\n");
                //Console.Write("To update items - enter 1");
                //Console.Write("To display options - enter 2");
                //Console.Write("To display list's options - enter 3");
                //Console.Write("To exit - enter 4");


                int n = int.Parse(Console.ReadLine());
                programDelivry outsideChoice = (programDelivry)n;

                switch (outsideChoice)
                {
                    //To adding customer, drone, station or parcel
                    case programDelivry.addingOptions:

                        Console.WriteLine("To adding customer - enter 0\nTo adding drone - enter 1\nTo adding station - enter 2\nTo adding parcel - enter 3\n");
                        int k = int.Parse(Console.ReadLine());
                        Add addChoice = (Add)k;
                        DalObject.DalObject addTemp = new DalObject.DalObject();
                        switch (addChoice)
                        {
                            case Add.addCustomer:
                                addTemp.addCustomer();
                                break;
                            case Add.addDrone:
                                addTemp.addDrone();
                                break;
                            case Add.addStation:
                                addTemp.addStaion();
                                break;
                            case Add.addParcel:
                                addTemp.addParcel();
                                break;
                            default:
                                break;
                        }
                        break;
                
                    //To update item

                    case programDelivry.UpdateOptions:

                        int idParcel, idDrone, idStation;

                        Parcel tempParcel = new Parcel();
                        Drone tempDrone = new Drone();
                        Station tempStation = new Station();

                        Console.WriteLine("To update Assign parcel to drone - enter 0\nTo update pick up parcel by drone - enter 1\nTo update that delivery has arrived - enter 2\nTo send drone to charge in basic station - enter 3\nTo free drone from chraging - enter 4\n");
                        int m = int.Parse(Console.ReadLine());
                        Update updateChoice = (Update)m;

                        DalObject.DalObject updateTemp = new DalObject.DalObject();
                      
                        switch (updateChoice)
                        {
                            case Update.assignParcelDrone:

                                Console.WriteLine("Please enter parcel's id:");
                                idParcel = int.Parse(Console.ReadLine());
                                
                                Console.WriteLine("Please enter drone's id:");
                                idDrone = int.Parse(Console.ReadLine());

                                tempParcel = updateTemp.findParcel(idParcel);
                                tempDrone = updateTemp.findDrone(idDrone);

                                updateTemp.assign_parcel_drone(ref tempParcel, ref tempDrone);

                                break;

                            case Update.dronePickParcel:

                                Console.WriteLine("Please enter parcel's id:");
                                idParcel = int.Parse(Console.ReadLine());

                                Console.WriteLine("Please enter drone's id:");
                                idDrone = int.Parse(Console.ReadLine());

                                tempParcel = updateTemp.findParcel(idParcel);
                                tempDrone = updateTemp.findDrone(idDrone);

                                updateTemp.drone_pick_parcel(ref tempParcel, ref tempDrone);

                                break;

                            case Update.deliveryAriveToCustomer:

                                Console.WriteLine("Please enter parcel's id:");
                                idParcel = int.Parse(Console.ReadLine());

                                Console.WriteLine("Please enter drone's id:");
                                idDrone = int.Parse(Console.ReadLine());

                                tempParcel = updateTemp.findParcel(idParcel);
                                tempDrone = updateTemp.findDrone(idDrone);

                                updateTemp.delivery_arrive_toCustomer(ref tempParcel, ref tempDrone);

                                break;

                            case Update.chargingDrone:

                                Console.WriteLine("Please enter drone's id:");
                                idDrone = int.Parse(Console.ReadLine());

                                Console.WriteLine("Please enter station's id:");
                                idStation = int.Parse(Console.ReadLine());

                                tempDrone = updateTemp.findDrone(idDrone);
                                tempStation = updateTemp.findStation(idStation);
                                DroneCharge tempDroneCharge = new DroneCharge();

                                updateTemp.chargingDrone(ref tempDrone, ref tempStation, ref tempDroneCharge);
                                break;

                            case Update.freeDroneCharge:

                                Console.WriteLine("Please enter drone's id:");
                                idDrone = int.Parse(Console.ReadLine());

                                Console.WriteLine("Please enter station's id:");
                                idStation = int.Parse(Console.ReadLine());

                                tempDrone = updateTemp.findDrone(idDrone);
                                tempStation = updateTemp.findStation(idStation);
                                
                                DroneCharge temp_DroneCharge = new DroneCharge();

                                updateTemp.freeDroneCharge(ref tempDrone, ref tempStation, ref temp_DroneCharge);
                                break;

                            default:
                                break;
                        }
                        break;

                    case programDelivry.DisplayOptions:
                        Console.WriteLine("To display customer - enter 0\nTo display drone - enter 1\nTo display station - enter 2\nTo display parcel - enter 3\n");
                        int j = int.Parse(Console.ReadLine());
                        Display displayChoice = (Display)j;
                        DalObject.DalObject displayTemp = new DalObject.DalObject();

                        switch (displayChoice)
                        {
                            case Display.displayCustomer:

                                Console.WriteLine("please enter customer's id:");
                                displayTemp.printCustomer(int.Parse(Console.ReadLine()));
                                break;

                            case Display.displayDrone:

                                Console.WriteLine("please enter drone's id:");
                                displayTemp.printDrone(int.Parse(Console.ReadLine()));
                                break;

                            case Display.displayParcel:

                                Console.WriteLine("please enter parcel's id:");
                                displayTemp.printParcel(int.Parse(Console.ReadLine()));
                                break;

                            case Display.displayStation:

                                Console.WriteLine("please enter station's id:");
                                displayTemp.printStation(int.Parse(Console.ReadLine()));
                                break;

                            default: break;
                        }
                        break;

                    case programDelivry.DisplayListOptions:

                        Console.WriteLine("To display customers - enter 0\nTo display drones - enter 1\nTo display stations - enter 2\nTo display parcels - enter 3\nTo displays a list of parcels without assign to drones - enter 4\nTo display base stations with available charging drones - enter 5\n");
                        int t = int.Parse(Console.ReadLine());
                        DisplayListOptions Display_list_choice = (DisplayListOptions)t;
                        DalObject.DalObject Display_List_options = new DalObject.DalObject();

                        switch (Display_list_choice)
                        {
                            case DisplayListOptions.displayCustomers:i

                                Display_List_options.printAllCustomers();
                                break;

                            case DisplayListOptions.displayDrones:

                                Display_List_options.printAllDrones();
                                break;

                            case DisplayListOptions.displayParcels:

                                Display_List_options.printAllParcels();
                                break;

                            case DisplayListOptions.displayStations:
                             
                                Display_List_options.printAllStations();
                                break;

                            case DisplayListOptions.display_parcels_without_drone:

                                Display_List_options.print_unconnected_parcels_to_Drone();
                                break;

                            case DisplayListOptions.display_station_with_freeChargingStations:

                                Display_List_options.print_stations_with_freeDroneCharge();
                                break;

                            default: break;
                        }
                        break;

                    case programDelivry.exit:
                        flag = false;
                        break;

                    default: break;
                }

            }



        }
    }



}

