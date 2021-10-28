using System;
using IDAL.DO;
using DAL;
using DalObject;

namespace ConsoleUI
{
    enum programDelivry { addingOptions, UpdateOptions, DisplayOptions, ListDisplayOptions, exit }
    enum Add { addCustomer, addDrone, addStation, addParcel };


    class Program
    {
        static void Main(string[] args)
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("To adding customer, drone, station or parcel - enter 0\n");
                Console.WriteLine("To update items - enter 1\n");
                Console.WriteLine("To update items enter 1");



                int n = int.Parse(Console.ReadLine());
                programDelivry outsideChoice = (programDelivry)n;

                switch (outsideChoice)
                {
                    case programDelivry.addingOptions:
                        int k = int.Parse(Console.ReadLine());
                        programDelivry insideChoice = (programDelivry)k;

                        switch (insideChoice)
                        {
                            case Add.addCustomer:
                                DalObject.DalObject a = new DalObject.DalObject();
                                a.addCustomer();
                                break;
                            case Add.addDrone:
                                break;
                            case Add.addStation:
                                break;
                            case Add.addParcel:
                                break;
                            default:
                                Console.WriteLine("Please enter again:");
                                break;
                        }
                        break;
                    case programDelivry.UpdateOptions:
                        break;
                    case programDelivry.DisplayOptions:
                        break;
                    case programDelivry.ListDisplayOptions:
                        break;
                    case programDelivry.exit:
                        break;
                    default:
                        break;
                }


                Console.WriteLine("To add customer - enter 0\nTo add drone - enter 1\nTo add station - enter 2\nTo add parcel - enter 3\n");
                int choiceAdd = int.Parse(Console.ReadLine());



            }



        }
    }



}

