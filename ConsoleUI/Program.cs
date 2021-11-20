/* Ester Shmuel ID:318968468 && Orly Sellem ID:315208728
  Lecturer: Adina Milston
  Course: .Dot Net
  Exercise: 2
  The purpose of program is to menege the inquiries from the user and routing his choices
 */
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


                int n = int.Parse(Console.ReadLine());//number absorption for choice
                Console.WriteLine();
                programDelivry outsideChoice = (programDelivry)n;//casting to programDelivry
                
                switch (outsideChoice)//the external switch (Menu of menus)
                {
                    
                    case programDelivry.addingOptions:  //To add customer, drone, station or parcel
                        Console.WriteLine("To add customer - enter 0\nTo add drone - enter 1\nTo add station - enter 2\nTo add parcel - enter 3");
                        int k = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        Add addChoice = (Add)k;//casting to Add

                        switch (addChoice)
                        {
                            case Add.addCustomer://add customer
                                mainDalObject.addCustomer();
                                break;
                            case Add.addDrone://add drone
                                mainDalObject.addDrone();
                                break;
                            case Add.addStation://add station
                                mainDalObject.addStaion();
                                break;
                            case Add.addParcel://add parcel
                                mainDalObject.addParcel();
                                break;
                            default:
                                break;
                        }
                        break;

                   
                   case programDelivry.UpdateOptions:   //To update item            
                        Console.WriteLine("To assign parcel to drone - enter 0\nTo pick up parcel by drone - enter 1\nTo update that delivery has arrived - enter 2\nTo send drone to charge in base station - enter 3\nTo free drone from chraging - enter 4\n");
                        int m = int.Parse(Console.ReadLine());
                        Update updateChoice = (Update)m;
                        switch (updateChoice)
                        {
                            case Update.assignParcelDrone://To assign parcel to drone
                                mainDalObject.assign_parcel_drone();
                                break;

                            case Update.dronePickParcel: //To pick up parcel by drone              
                                mainDalObject.drone_pick_parcel();
                                break;

                            case Update.deliveryAriveToCustomer:  //To update that delivery has arrived                              
                                mainDalObject.delivery_arrive_toCustomer();
                                break;

                            case Update.chargingDrone://To send drone to charge in base station
                                mainDalObject.chargingDrone();
                                break;

                            case Update.freeDroneCharge://To free drone from chraging
                                mainDalObject.freeDroneCharge();
                                break;

                            default:
                                break;
                        }
                        break;

                    case programDelivry.DisplayOptions://To print the display options
                        Console.WriteLine("To display customer - enter 0\nTo display drone - enter 1\nTo display station - enter 2\nTo display parcel - enter 3\n");
                        int j = int.Parse(Console.ReadLine());
                        Display displayChoice = (Display)j;

                        switch (displayChoice) //menu accordding to user's choice - display spefific entity
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

                    case programDelivry.DisplayListOptions://menu accordding to user's choice - display all the array of entity

                        Console.WriteLine("To display customers - enter 0\nTo display drones - enter 1\nTo display stations - enter 2\nTo display parcels - enter 3\nTo displays a list of parcels without assign to drones - enter 4\nTo display base stations with available charging drones - enter 5\n");
                        int t = int.Parse(Console.ReadLine());
                        DisplayListOptions Display_list_choice = (DisplayListOptions)t;

                        switch (Display_list_choice)//menu accordding to the user's choice - display list choice
                        {
                            case DisplayListOptions.displayCustomers://to print all the customers's details

                                mainDalObject.printAllCustomers();
                                break;

                            case DisplayListOptions.displayDrones://to print all the drones's details

                                mainDalObject.printAllDrones();
                                break;

                            case DisplayListOptions.displayParcels://to print all the parcels's details

                                mainDalObject.printAllParcels();
                                break;

                            case DisplayListOptions.displayStations://to print all the stations's details

                                mainDalObject.printAllStations();
                                break;

                            case DisplayListOptions.display_parcels_without_drone://to print all the parcels without drone

                                mainDalObject.print_unconnected_parcels_to_Drone();
                                break;

                            case DisplayListOptions.display_station_with_freeChargingStations://to print all the parcels with free charging stations

                                mainDalObject.print_stations_with_freeDroneCharge();
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

