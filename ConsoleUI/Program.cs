/* Ester Shmuel ID:318968468 && Orly Sellem ID:315208728
  Lecturer: Adina Milston
  Course: .Dot Net
  Exercise: 2
  The purpose of program is to menege the inquiries from the user and routing his choices
 */
using System;
using IDAL.DO;
using IDAL.DO;
using DalObject;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {

            bool flag = true;
            // DalObject.DalObject mainDalObject = new DalObject.DalObject();
            DalObject.DalObject mainDalObject =  new DalObject.DalObject();
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

                    case programDelivry.addingOptions:
                        //To add customer, drone, station or parcel
                        Console.WriteLine("To add customer - enter 0\nTo add drone - enter 1\nTo add station - enter 2\nTo add parcel - enter 3");
                        int k = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        Add addChoice = (Add)k;//casting to Add

                        int id, maxWeight,idDrone, idParcel, idStation;
                        double longitude, lattitude;

                        switch (addChoice)
                        {
                            case Add.addCustomer:     //add customer
                                //Ask the user to insert the customer's details
                                Console.WriteLine("Please enter your id:");
                                id = int.Parse(Console.ReadLine());
                                Console.WriteLine("Please enter your name:");
                                string name_st = Console.ReadLine();
                                Console.WriteLine("Please enter your phone:");
                                string phone = Console.ReadLine();
                                Console.WriteLine("Please enter lattitude:");
                                lattitude = double.Parse(Console.ReadLine());
                                Console.WriteLine("Please enter longitude:");
                                longitude = double.Parse(Console.ReadLine());
                                Console.WriteLine();

                                mainDalObject.addCustomer(id, name_st, phone, longitude, lattitude);

                                break;


                            case Add.addDrone://add drone

                                //Ask the user to insert the drone's details
                                Console.WriteLine("Please enter drone's id:");
                                id = int.Parse(Console.ReadLine());
                                Console.WriteLine("Please enter drone's model:");
                                string model = Console.ReadLine();
                                Console.WriteLine("Please enter drone's weight categories - 0 for light, 1 for medium, 2 for heavy:");
                                maxWeight = int.Parse(Console.ReadLine());

                                mainDalObject.addDrone(id, model, maxWeight);
                                break;

                            case Add.addStation://add station

                                //Ask the user to insert the station's details
                                Console.WriteLine("Please enter station's id:");
                                id = int.Parse(Console.ReadLine());

                                Console.WriteLine("Please enter station's name:");
                                int name_int = int.Parse(Console.ReadLine());

                                Console.WriteLine("Please enter the station's longitude");
                                longitude = double.Parse(Console.ReadLine());

                                Console.WriteLine("Please enter the station's lattitude");
                                lattitude = double.Parse(Console.ReadLine());

                                Console.WriteLine("Please enter the station's chargeSlots");
                                int chargeSlots = int.Parse(Console.ReadLine());

                                mainDalObject.addStaion(id, name_int, longitude, lattitude, chargeSlots);
                                break;

                            case Add.addParcel://add parcel

                                //Ask the user to insert the parcel's details
                                Console.WriteLine("Please enter sender's id:");
                                int senderld = int.Parse(Console.ReadLine());

                                Console.WriteLine("Please enter target's id:");
                                int targetld = int.Parse(Console.ReadLine());

                                Console.WriteLine("Please enter drone's weight categories - 0 for light, 1 for medium, 2 for heavy:");
                                maxWeight = int.Parse(Console.ReadLine());

                                Console.WriteLine("Please enter the delivery's priority - 0 for normal, 1 for fast, 2 for emergency:");
                                int priority = int.Parse(Console.ReadLine());
                                Console.WriteLine();

                                mainDalObject.addParcel(senderld, targetld, maxWeight, priority);
                                
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

                                Console.WriteLine("Please enter drone's id:");
                                idDrone = int.Parse(Console.ReadLine());
                                Console.WriteLine("Please enter parcel's id:");
                                idParcel = int.Parse(Console.ReadLine()); 

                                mainDalObject.assign_parcel_drone(idDrone, idParcel);
                                break;

 
                            case Update.deliveryAriveToCustomer:  //To update that delivery has arrived

                                Console.WriteLine("Please enter drone's id:");
                                idDrone = int.Parse(Console.ReadLine());
                                Console.WriteLine("Please enter parcel's id:");
                                idParcel = int.Parse(Console.ReadLine());      
                                
                                mainDalObject.delivery_arrive_toCustomer(idDrone, idParcel);
                                break;

                            case Update.chargingDrone://To send drone to charge in base station

                                 Console.WriteLine("Please enter drone's id:");
                                idDrone = int.Parse(Console.ReadLine());
                                Console.WriteLine("Please enter station's id:");
                                idStation = int.Parse(Console.ReadLine()); 
                                
                                mainDalObject.chargingDrone(idDrone, idStation);
                                break;

                            case Update.freeDroneCharge://To free drone from chraging

                                Console.WriteLine("Please enter drone's id:");
                                idDrone = int.Parse(Console.ReadLine());
                                Console.WriteLine("Please enter station's id:");
                                idStation = int.Parse(Console.ReadLine());   
                                
                                mainDalObject.freeDroneCharge(idDrone, idStation);
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

                                /*mainDalObject.returnArrCustomers();
                                 foreach (Customer item in DataSource.customers)
                                 {
                                      if (item.Id == 0)
                                         break;
                                  Console.WriteLine(item.ToString());
                                 }*/

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

      /*public void printStation(int id)//print  the requested Station
        {
            foreach (var item in DataSource.stations)
            {
                if (item.Id == id)
                {
                    Console.WriteLine(item.ToString());
                    break;
                }
            }

        }

        public void printDrone(int id)//print the requested Drone
        {
            foreach (var item in DataSource.drones)
            {
                if (item.Id == id)
                {
                    Console.WriteLine(item.ToString());
                    break;
                }
            }

        }

        public void printCustomer(int id)//print the requested Customer
        {
            

        }

        public void printParcel(int id)//print the requested parcel
        {

            foreach (var item in DataSource.parcels)
            {
                if (item.Id == id)
                {
                    Console.WriteLine(item.ToString());
                    break;
                }

            }

        }

        public void printAllStations()
        {
            foreach (Station item in DataSource.stations)
            {
                if (item.Id == 0)
                    break;
                Console.WriteLine(item.ToString());
            }

        }

        public void printAllDrones()
        {
            foreach (Drone item in DataSource.drones)
            {
                if (item.Id == 0)
                    break;
                Console.WriteLine(item.ToString());
            }

        }

        public void printAllCustomers()
        {
            foreach (Customer item in DataSource.customers)
            {
                if (item.Id == 0)
                    break;
                Console.WriteLine(item.ToString());
            }

        }

        public void printAllParcels()
        {
            foreach (Parcel item in DataSource.parcels)
            {
                if (item.Id == 0)
                    break;
                Console.WriteLine(item.ToString());
            }
        }

        public void print_unconnected_parcels_to_Drone()
        {
            foreach (Parcel item in DataSource.parcels)
            {
                if (item.Droneld == 0 && item.Id != 0)
                    Console.WriteLine(item.ToString());
            }
        }

        public void print_stations_with_freeDroneCharge()
        {
            foreach (Station item in DataSource.stations)
            {
                if (item.ChargeSlots > 0 && item.Id != 0)
                    Console.WriteLine(item.ToString());
            }
        }
    */


}

