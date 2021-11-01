﻿using System;
using IDAL;
using DalObject;
using IDAL.DO;

namespace ConsoleUI
{
    class Program
    {

        enum MenuOptions { Add = 1, Update, Show_One, Show_List, Exit }
        enum EntityOptions { BaseStation = 1, Drone, Custumer, Parcel, Exit }
        enum UpdateOptions { Assignement = 1, Pickedup, Delivery, Recharge, Discharge, Exit }
        enum ListOptions { BaseStation = 1, Drone, Custumer, Parcel, UnAsignementParcel, AvailbleChagingStation, Exit }

        private static void ShowMenu()
        {

            MenuOptions menuOptions;
            EntityOptions entityOptions;
            UpdateOptions updateOptions;
            DalObject.DalObject dalobject = new DalObject.DalObject();



            do
            {
                Console.WriteLine("WELCOME!");
                Console.WriteLine("option:\n 1- Add,\n 2- Update,\n 3- Show_One,\n 4- Show_List,\n 5- Exit,\n");
                menuOptions = (MenuOptions)int.Parse(Console.ReadLine());
                switch (menuOptions)
                {
                    case MenuOptions.Add:
                        Console.WriteLine("Adding option:\n 1-BaseStation,\n 2-Drone,\n 3-Custumer,\n 4-Parcel,\n 5-Exit");

                        entityOptions = (EntityOptions)int.Parse(Console.ReadLine());
                        switch (entityOptions)
                        {
                            case EntityOptions.BaseStation:
                                Station s = new Station();
                                Console.WriteLine("Please insert ID, StationName (string), longitude, latitude and charging level ");
                                int id_S;
                                int.TryParse(Console.ReadLine(), out id_S);
                                string StationName = Console.ReadLine();
                                s.Name = StationName;
                                double longitude;
                                double.TryParse(Console.ReadLine(), out longitude);
                                double latitude;
                                double.TryParse(Console.ReadLine(), out latitude);
                                int Position;
                                int.TryParse(Console.ReadLine(), out Position);

                                s.Id = id_S;
                                s.Name = StationName;
                                s.Latitude = latitude;
                                s.Longitude = longitude;
                                s.ChargeSlots = Position;
                                dalobject.AddBaseStation(s);
                                Console.WriteLine("\nBase station added successfully! \n");
                                break;

                            case EntityOptions.Drone:
                                Console.WriteLine("please enter ID, Model (string), Weight categories, Status and Batery level (double)");
                                int id_D;
                                int.TryParse(Console.ReadLine(), out id_D);
                                string model = Console.ReadLine();
                                Console.WriteLine("enter WeightCategories 1-Light 2-Medium, 3-Heavy");
                                int Weight;
                                int.TryParse(Console.ReadLine(), out Weight);
                                Console.WriteLine("enter DroneStatuses 1-Available 2-Maintenance, 3-Shipping");
                                int status;
                                int.TryParse(Console.ReadLine(), out status);
                                double battery;
                                double.TryParse(Console.ReadLine(), out battery);

                                Drone d = new Drone();

                                d.Id = id_D;
                                d.Model = model;
                                d.Battery = battery;
                                d.Status = (DroneStatuses)status;
                                d.MaxWeight = (WeightCategories)Weight;
                                dalobject.AddDrone(d);
                                Console.WriteLine("\nDrone added successfully! \n");
                                break;

                            case EntityOptions.Custumer:
                                Console.WriteLine("please enter Customer ID, Name, Phone number, Longitude and Latitude ");
                                int id_C;
                                int.TryParse(Console.ReadLine(), out id_C);
                                string name_C = Console.ReadLine();
                                //נצטרך לבדוק תקינות של מס טלפון
                                Console.WriteLine("enter phone number");
                                string phone_C = Console.ReadLine();
                                Console.WriteLine("enter longitude and latitude");
                                double longitude_C;
                                double.TryParse(Console.ReadLine(), out longitude_C);
                                double latitude_C;
                                double.TryParse(Console.ReadLine(), out latitude_C);

                                Customer c = new Customer();
                                c.Id = id_C;
                                c.Name = name_C;
                                c.Phone = phone_C;
                                c.Latitude = latitude_C;
                                c.Longitude = longitude_C;
                                dalobject.AddCustomer(c);
                                Console.WriteLine("\nCustomer added successfully! \n");
                                break;

                            case EntityOptions.Parcel:
                                Console.WriteLine("Please enter parcel ID");
                                int id_P;
                                int.TryParse(Console.ReadLine(), out id_P);
                                Console.WriteLine("Please enter the sender's ID");
                                int id_Psender;
                                int.TryParse(Console.ReadLine(), out id_Psender);
                                Console.WriteLine("Please enter target ID");
                                int id_Ptarget;
                                int.TryParse(Console.ReadLine(), out id_Ptarget);
                                Console.WriteLine("Please enter parcel weight: 1-Light, 2-Medium, 3-Heavy");
                                int weight_P;
                                int.TryParse(Console.ReadLine(), out weight_P);
                                Console.WriteLine("Please enter parcel priority: 1-Regular, 2-Fast, 3-Emergency");
                                int priority_P;
                                int.TryParse(Console.ReadLine(), out priority_P);
                                Console.WriteLine("Please enter drone ID");
                                int id_Pdrone;
                                int.TryParse(Console.ReadLine(), out id_Pdrone);
                                Console.WriteLine("Please enter time to prepare a package for delivery in format ##:##");
                                DateTime requested_P;
                                DateTime.TryParse(Console.ReadLine(), out requested_P);
                                /**
                                 * מתי מוכנסים הזמנים האלה?
                                DateTime steduled_P;
                                DateTime.TryParse(Console.ReadLine(), out steduled_P);
                                DateTime pickedUp_P;
                                DateTime.TryParse(Console.ReadLine(), out pickedUp_P);
                                DateTime delivary_P;
                                DateTime.TryParse(Console.ReadLine(), out delivary_P);
                                **/
                                Parcel p = new Parcel();
                                p.Id = id_P;
                                p.SenderId = id_Psender;
                                p.TargetId = id_Ptarget;
                                p.Weight = (WeightCategories)weight_P;
                                p.Priority = (Priorities)priority_P;
                                p.Requested = requested_P;
                                p.DroneID = id_Pdrone;
                                /**
                                p.Scheduled = steduled_P;
                                p.PickedUp = pickedUp_P;
                                p.Delivered = delivary_P;
                                **/

                                dalobject.AddParcel(p);
                                Console.WriteLine("\nParcel added successfully! \n");
                                break;

                            case EntityOptions.Exit:
                                DalObject.DalObject.Exit();
                                break;
                        }
                        break;

                    case MenuOptions.Update:
                        {
                            Console.WriteLine("Updating option:\n 1-Parcel to drone,\n 2-Parcel pickedup by drone,\n 3-Supply parcel to customer,\n 4-Send drone to charge,\n 5-Discharge drone, \n 6- Exit");
                            updateOptions = (UpdateOptions)int.Parse(Console.ReadLine());
                            switch (updateOptions)
                            {
                                case UpdateOptions.Assignement:
                                    Console.WriteLine("Please enter Drone ID");
                                    int drone_id;
                                    int.TryParse(Console.ReadLine(),out drone_id);
                                    Console.WriteLine("Please enter Parcel ID");
                                    int parcel_id;
                                    int.TryParse(Console.ReadLine(),out parcel_id);
                                    dalobject.UpdateParcelToDrone(parcel_id, drone_id);
                                    Console.WriteLine("\nParcel updated to drone successfully!\n");
                                    break;

                                case UpdateOptions.Pickedup:
                                    Console.WriteLine("Please enter Drone ID");
                                    int drone_id2;
                                    int.TryParse(Console.ReadLine(),out drone_id2);
                                    Console.WriteLine("Please enter Parcel ID");
                                    int parcel_id2;
                                    int.TryParse(Console.ReadLine(),out parcel_id2);
                                    dalobject.UpdateParcelPickedupByDrone(parcel_id2, drone_id2);
                                    Console.WriteLine("\nParcel pick up updated successfully!\n");
                                    break;

                                case UpdateOptions.Delivery:
                                    Console.WriteLine("Please enter Customer ID");
                                    int customer_id;
                                    int.TryParse(Console.ReadLine(),out customer_id);
                                    Console.WriteLine("Please enter Parcel ID");
                                    int parcel_id3;
                                    int.TryParse(Console.ReadLine(),out parcel_id3);
                                    dalobject.UpdateDeliveryToCustomer(parcel_id3, customer_id);
                                    Console.WriteLine("\nParcel updated to customer successfully!\n");
                                    break;

                                case UpdateOptions.Recharge:
                                    Console.WriteLine("Please enter Drone ID");
                                    int drone_id4;
                                    int.TryParse(Console.ReadLine(),out drone_id4);
                                    Console.WriteLine("choose a station for charging");
                                    // show the list os stations to choose from
                                    dalobject.ShowBaseStationList();
                                    int station_id;
                                    int.TryParse(Console.ReadLine(),out station_id);
                                    dalobject.UpdateDroneToCharge(drone_id4, station_id);
                                    Console.WriteLine("\nDrone updated to- charge status successfully!\n");
                                    break;

                                case UpdateOptions.Discharge:
                                    Console.WriteLine("Please enter Drone ID");
                                    int drone_id5;
                                    int.TryParse(Console.ReadLine(),out drone_id5);
                                    Console.WriteLine("choose a station for charging");
                                    int station_id_discharge;
                                    int.TryParse(Console.ReadLine(),out station_id_discharge);
                                    dalobject.DischargeDrone(drone_id5, station_id_discharge);
                                    Console.WriteLine("\nDrone updated to- discharge status successfully!\n");
                                    break;

                                case UpdateOptions.Exit:
                                    DalObject.DalObject.Exit();
                                    break;
                            }
                            break;
                        }

                    // show options
                    case MenuOptions.Show_One:
                        Console.WriteLine("View item options: \n 1- base station \n 2- Drone\n 3- Custumer\n 4- Parcel\n 5- Exit\n");
                        entityOptions = (EntityOptions)int.Parse(Console.ReadLine());
                        Console.WriteLine($"Enter a requested {entityOptions} id");
                        switch(entityOptions)
                        {
                            case EntityOptions.BaseStation:
                                int Id_S;
                                int.TryParse(Console.ReadLine(), out Id_S);
                                dalobject.ShowBaseStation(Id_S);
                                break;
                            case EntityOptions.Drone:
                                int Id_D;
                                int.TryParse(Console.ReadLine(), out Id_D);
                                dalobject.ShowDrone(Id_D);
                                break;
                            case EntityOptions.Custumer:
                                int Id_C;
                                int.TryParse(Console.ReadLine(), out Id_C);
                                dalobject.ShowCustomer(Id_C);
                                break;
                            case EntityOptions.Parcel:
                                int Id_P;
                                int.TryParse(Console.ReadLine(), out Id_P);
                                dalobject.ShowParcel(Id_P);
                                break;
                            case EntityOptions.Exit:
                                DalObject.DalObject.Exit();
                                break;
                        }
                        int requestion;
                        int.TryParse(Console.ReadLine(), out requestion);

                        break;
                    case MenuOptions.Show_List:
                        Console.WriteLine(" List options:\n 1- BaseStation  \n 2- Drone \n 3- Custumer\n 4- Parcel\n 5- UnAsignementParcel\n 6- AvailbleChagingStation\n 7- Exit \n");
                        ListOptions listOptions = (ListOptions)int.Parse(Console.ReadLine());
                        switch (listOptions)
                        {
                            case ListOptions.BaseStation:
                                dalobject.ShowBaseStationList();
                                break;
                            case ListOptions.Drone:
                                dalobject.ShowDroneList();
                                break;
                            case ListOptions.Custumer:
                                dalobject.ShowCustomerList();
                                break;
                            case ListOptions.Parcel:
                                dalobject.ShowParcelList();
                                break;
                            case ListOptions.AvailbleChagingStation:
                                dalobject.ShowNonAssociatedParcelList();
                                break;
                            case ListOptions.UnAsignementParcel:
                                dalobject.ShowChargeableBaseStationList();
                                break;
                        }
                        break;
                    case MenuOptions.Exit:
                        break;
                }

            }
            while (menuOptions != MenuOptions.Exit);
        }
        static void Main(string[] arg)
        {
            new DalObject.DalObject();
            ShowMenu();
        }
    
    } 
}
         