using System;
using IBL;
using IBL.BO;
namespace ConsoleUI_BL
{
    class Program
    {
        static void Main(string[] args)
        {

            IBL.BL mainBl = new IBL.BL();
            bool flag = true;

            //Variable of station
            int id, name_int, chargeSlots;
            double longitude, lattitude;
            string name_st, phone;

            while (flag)
            {
                try
                {
                    int n = int.Parse(Console.ReadLine());
                    programDelivry outsideChoice = (programDelivry)n;

                    switch (outsideChoice)
                    {
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
                                        Name=name_int,
                                        Address= stationLocation, 
                                        ChargeSlots= chargeSlots,
                                    };


                                    mainBl.addStation(newStation);

                                    mainBl.chargingDrone ={ }; //רשימת הרחפנים בטעינה תאותחל לרשימה ריקה
                                    break;
                                case Add.addDrone:

                                    //Ask the user to insert the drone's details
                                    Console.WriteLine("Please enter drone's id:");
                                    id = int.Parse(Console.ReadLine());

                                    Console.WriteLine("Please enter drone's model:");
                                    string model = Console.ReadLine();

                                    Console.WriteLine("Please enter drone's weight categories - 0 for light, 1 for medium, 2 for heavy:");
                                    int maxWeight = int.Parse(Console.ReadLine());

                                    int idStation = int.Parse(Console.ReadLine());
                                
                                    Random rand = new Random();
                                    
                                    Drone droneToAdd = new Drone()
                                    {
                                        Id = id,
                                        Model = model,
                                        MaxWeight = (WeightCategories)maxWeight,
                                        Battery = rand.Next(20, 40),
                                        Status=DroneStatuses.maintenance,                        
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

                                    Console.WriteLine();

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

                                    //Ask the user to insert the parcel's details
                              
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

                                    mainBl.addParcel();
                                    mainBl.resetTime();//כל הזמנים יאותחלו לזמן אפס למעט תאריך יצירה שיאותחל לעכשיו

                                    break;
                                default:
                                    break;
                            }
                            break;
                        case programDelivry.UpdateOptions:

                            Console.WriteLine("To assign parcel to drone - enter 0\nTo pick up parcel by drone - enter 1\nTo update that delivery has arrived - enter 2\nTo send drone to charge in base station - enter 3\nTo free drone from chraging - enter 4\n");
                            int m = int.Parse(Console.ReadLine());
                            Update updateChoice = (Update)m;

                            switch (updateChoice)
                            {
                                case Update.chargingDrone:
                                    id = int.Parse(Console.ReadLine());
                                    mainBl.chargingDrone();
                                    break; 

                                case Update.assignParcelDrone:
                                    break;
                                case Update.dronePickParcel:
                                    break;
                                case Update.deliveryAriveToCustomer:
                                    break;
                                
                                case Update.freeDroneCharge:
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case programDelivry.DisplayOptions:
                            break;
                        case programDelivry.DisplayListOptions:
                            break;
                        case programDelivry.exit:
                            break;
                        default:
                            break;
                    }
                }

                catch
                {

                }
            }

        }
    }
}
