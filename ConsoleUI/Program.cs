using System;
using IDAL.DO;
using DAL;
using DalObject;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            bool flag = true;
            DalObject.DalObject mainDalObject = new DalObject.DalObject();
            while (flag)
            {
                
                Console.WriteLine("To add customer, drone, station or parcel - enter 0");
                Console.WriteLine("To update items - enter 1");
                Console.WriteLine("To display options - enter 2");
                Console.WriteLine("To display list's options - enter 3");
                Console.WriteLine("To exit - enter 4");


                int n = int.Parse(Console.ReadLine());//number for choice
                Console.WriteLine();
                programDelivry outsideChoice = (programDelivry)n;

                switch (outsideChoice)
                {
                    //To add customer, drone, station or parcel
                    case programDelivry.addingOptions:
                        
                        Console.WriteLine("To add customer - enter 0\nTo add drone - enter 1\nTo add station - enter 2\nTo add parcel - enter 3");
                        int k = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        Add addChoice = (Add)k;

                        switch (addChoice)
                        {
                            case Add.addCustomer:
                                mainDalObject.addCustomer();
                                break;
                            case Add.addDrone:
                                mainDalObject.addDrone();
                                break;
                            case Add.addStation:
                                mainDalObject.addStaion();
                                break;
                            case Add.addParcel:
                                mainDalObject.addParcel();
                                break;
                            default:
                                break;
                        }
                        break;

                    //To update item

                    case programDelivry.UpdateOptions:              
                        Console.WriteLine("To assign parcel to drone - enter 0\nTo pick up parcel by drone - enter 1\nTo update that delivery has arrived - enter 2\nTo send drone to charge in base station - enter 3\nTo free drone from chraging - enter 4\n");
                        int m = int.Parse(Console.ReadLine());
                        Update updateChoice = (Update)m;
                        DroneCharge tempDroneCharge = new DroneCharge();
                        switch (updateChoice)
                        {
                            case Update.assignParcelDrone:
                                mainDalObject.assign_parcel_drone();
                                break;

                            case Update.dronePickParcel:               
                                mainDalObject.drone_pick_parcel();
                                break;

                            case Update.deliveryAriveToCustomer:                               
                                mainDalObject.delivery_arrive_toCustomer();
                                break;

                            case Update.chargingDrone:
                                mainDalObject.chargingDrone(ref tempDroneCharge);
                                break;

                            case Update.freeDroneCharge:
                                DroneCharge temp_DroneCharge = new DroneCharge();
                                mainDalObject.freeDroneCharge(ref temp_DroneCharge);
                                break;

                            default:
                                break;
                        }
                        break;

                    case programDelivry.DisplayOptions:
                        Console.WriteLine("To display customer - enter 0\nTo display drone - enter 1\nTo display station - enter 2\nTo display parcel - enter 3\n");
                        int j = int.Parse(Console.ReadLine());
                        Display displayChoice = (Display)j;

                        switch (displayChoice)
                        {
                            case Display.displayCustomer:

                                Console.WriteLine("please enter customer's id:");
                                mainDalObject.printCustomer(int.Parse(Console.ReadLine()));
                                break;

                            case Display.displayDrone:

                                Console.WriteLine("please enter drone's id:");
                                mainDalObject.printDrone(int.Parse(Console.ReadLine()));
                                break;

                            case Display.displayParcel:

                                Console.WriteLine("please enter parcel's id:");
                                mainDalObject.printParcel(int.Parse(Console.ReadLine()));
                                break;

                            case Display.displayStation:

                                Console.WriteLine("please enter station's id:");
                                int num = int.Parse(Console.ReadLine());
                                mainDalObject.printStation(num);
                                break;

                            default: break;
                        }
                        break;

                    case programDelivry.DisplayListOptions:

                        Console.WriteLine("To display customers - enter 0\nTo display drones - enter 1\nTo display stations - enter 2\nTo display parcels - enter 3\nTo displays a list of parcels without assign to drones - enter 4\nTo display base stations with available charging drones - enter 5\n");
                        int t = int.Parse(Console.ReadLine());
                        DisplayListOptions Display_list_choice = (DisplayListOptions)t;

                        switch (Display_list_choice)
                        {
                            case DisplayListOptions.displayCustomers:

                                mainDalObject.printAllCustomers();
                                break;

                            case DisplayListOptions.displayDrones:

                                mainDalObject.printAllDrones();
                                break;

                            case DisplayListOptions.displayParcels:

                                mainDalObject.printAllParcels();
                                break;

                            case DisplayListOptions.displayStations:

                                mainDalObject.printAllStations();
                                break;

                            case DisplayListOptions.display_parcels_without_drone:

                                mainDalObject.print_unconnected_parcels_to_Drone();
                                break;

                            case DisplayListOptions.display_station_with_freeChargingStations:

                                mainDalObject.print_stations_with_freeDroneCharge();
                                break;

                            default: break;
                        }
                        break;

                    /*case programDelivry.distance:
                        
                        Console.WriteLine("Please enter a coordinate:");
                        double a = double.Parse(Console.ReadLine());
                        double b = double.Parse(Console.ReadLine());
                        Console.WriteLine("To check distance from customer - enter 0\nTo check distance from station - enter 1\n:");
                        int temp = int.Parse(Console.ReadLine());
                        if (temp == 0)
                        {
                            Console.WriteLine("Please enter customer's id:");
                            int id = int.Parse(Console.ReadLine());
                            Customer chosen = mainDalObject.findCustomer(id);
                            Console.WriteLine(Math.Sqrt(Math.Pow(a - chosen.Lattitude, 2) + Math.Pow(b - chosen.Longitude, 2)));
                            Console.WriteLine();
                        }
                        else if (temp == 1)
                        {
                            Console.WriteLine("Please enter station's id:");
                            int id = int.Parse(Console.ReadLine());
                            Station chosen = mainDalObject.findStation(id);
                            Console.WriteLine(Math.Sqrt(Math.Pow(a - chosen.Lattitude, 2) + Math.Pow(b - chosen.Longitude, 2)));
                            Console.WriteLine();
                        }
                    */
                    case programDelivry.exit:
                        flag = false;
                        break;

                    default: break;
                }

            }



        }
    }



}

