﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLXML;
using DalApi;
using DO;

namespace DXML
{
    class DalXML//:IDal
    {
        //string dronePath = "drons.xml";

        //#region singelton
        //static readonly DXML instance = new DXML();
        //static DXML() { }// static ctor to ensure instance init is done just before first usage
        //DXML() { } // default => private
        //public static DXML Instance { get => instance; }// The public Instance property to use
        //#endregion

        //#region Coustumer
        ///// <summary>
        ///// add Customer to the Customers list
        ///// </summary>
        ///// <param name="c"></param>
        ///// <returns></returns>
        //public void AddCustomer(Customer c)
        //{
        //    if (DataSource.Customer.Exists(client => client.Id == c.Id))
        //    {
        //        throw new CustomerException($"ID {c.Id} already exists!!");
        //    }
        //    else
        //        DataSource.Customer.Add(c);
        //}
        ///// <summary>
        ///// update customee name and phone---
        ///// </summary>
        ///// <param name="custumerId"></param>
        ///// <param name="name"></param>
        ///// <param name="phone"></param>
        //public void UpdateCustumer(int custumerId, string name, string phone)
        //{
        //    if (!DataSource.Customer.Exists(x => x.Id == custumerId))
        //    {
        //        throw new Exception($"custumer {custumerId} is not exite!!");
        //    }
        //    Customer customer = DataSource.Customer.Find(x => x.Id == custumerId);
        //    DataSource.Customer.Remove(customer);
        //    customer.Name = name;
        //    customer.Phone = phone;
        //    DataSource.Customer.Add(customer);
        //}
        ///// <summary>
        ///// Get Customer by id
        ///// </summary>
        ///// <param name="id"></param>
        //public Customer GetCustomer(int IDc)
        //{
        //    //if ID does not exist for customer
        //    if (!DataSource.Customer.Exists(item => item.Id == IDc))
        //    {
        //        throw new CustomerException($"ID: {IDc} does not exist!!");
        //    }
        //    return DataSource.Customer.First(c => c.Id == IDc);
        //}

        ///// <summary>
        ///// Show list of Customers
        ///// </summary>
        //public IEnumerable<Customer> ShowCustomerList(Func<Customer, bool> predicate = null)
        //{
        //    if (predicate == null)
        //    {
        //        List<Customer> CustomerList = new();
        //        foreach (Customer element in DataSource.Customer)
        //        {
        //            CustomerList.Add(element);
        //        }
        //        return CustomerList;
        //    }
        //    else
        //        return DataSource.Customer.Where(predicate).ToList();
        //}
        //#endregion

        //#region Drone
        ///// <summary>
        ///// add Drone to the drons list
        ///// </summary>
        ///// <param name="d"></param>
        ///// <returns></returns>
        //public void AddDrone(Drone d)
        //{
        //    List<DO.Drone> dronsList = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
        //    if (dronsList.Exists(drone => drone.Id == d.Id))
        //    {
        //        throw new DroneException($"ID {d.Id} already exists!!");
        //    }
        //    else
        //    {
        //        dronsList.Add(d);
        //        XMLTools.SaveListToXMLSerializer<Drone>(dronsList, dronePath);
        //    }

        //}
        ///// <summary>
        ///// Update name of drone
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="model"></param>
        //public void UpdateNameOfDrone(int id, string model)
        //{
        //    Drone drone = DataSource.Drones.Find(x => x.Id == id);
        //    DataSource.Drones.Remove(drone);
        //    drone.Model = model;
        //    DataSource.Drones.Add(drone);
        //}
        ///// <summary>
        ///// view function for Drone
        ///// </summary>
        ///// <param name="id"></param>
        //public Drone GetDrone(int id)
        //{
        //    if (!DataSource.Drones.Exists(item => item.Id == id))
        //    {
        //        throw new DroneException($"ID: {id} does not exist!!");
        //    }
        //    return DataSource.Drones.Find(c => c.Id == id);
        //}
        ///// <summary>
        ///// view lists functions for Drone
        ///// </summary>
        //public IEnumerable<Drone> ShowDroneList(Func<Drone, bool> predicate = null)
        //{
        //    if (predicate == null)
        //    {
        //        List<Drone> DroneList = new List<Drone>();
        //        foreach (Drone element in DataSource.Drones)
        //        {
        //            DroneList.Add(element);
        //        }

        //        return DroneList;
        //    }
        //    else
        //        return DataSource.Drones.Where(predicate).ToList();
        //}

        ///// <summary>
        ///// Send a drone to charge
        ///// </summary>
        ///// <param name="droneId">the drone to send to charge</param>
        ///// <param name="stationId">the station to send it to charge</param>
        //public void SendDroneToCharge(int droneId, int stationId)
        //{
        //    Drone drone = GetDrone(droneId);
        //    Station station = GetStation(stationId);
        //    DataSource.Stations.Remove(station);

        //    DataSource.DroneCharges.Add(new DroneCharge
        //    {
        //        DroneId = drone.Id,
        //        StationId = station.Id
        //    });
        //    station.ChargeSpots--;

        //    DataSource.Stations.Add(station);
        //}
        ///// <summary>
        ///// release a drone from charge
        ///// </summary>
        ///// <param name="droneId">the id of the drone to release</param>
        //public void ReleaseDroneFromCharging(int droneId)
        //{
        //    Drone drone = GetDrone(droneId);
        //    DataSource.Drones.Remove(drone);

        //    DroneCharge droneCharge = DataSource.DroneCharges.Find(x => x.DroneId == droneId);


        //    int stationId = droneCharge.StationId;
        //    Station station = GetStation(stationId);
        //    DataSource.Stations.Remove(station);

        //    station.ChargeSpots++;

        //    DataSource.Stations.Add(station);
        //    DataSource.Drones.Add(drone);
        //    DataSource.DroneCharges.Remove(droneCharge);

        //}
        ///// <summary>
        ///// discharge drone
        ///// <param name="droneID"></param>
        ///// <param name="droneLatitude"></param>
        ///// <param name="droneLongitude"></param>
        ///// <exception cref="Exception"></exception>
        //public Station DischargeDroneByLocation(int droneID, double droneLatitude, double droneLongitude)
        //{

        //    Drone d = DataSource.Drones.Find(x => x.Id == droneID);
        //    Station s = new Station();
        //    foreach (Station item in DataSource.Stations) //finds the station
        //    {
        //        if (item.Latitude == droneLatitude && item.Longitude == droneLongitude)
        //        {
        //            DataSource.Stations.Remove(s);
        //            s = item;
        //            s.ChargeSpots++;
        //            DataSource.Stations.Add(s);
        //            return s;
        //        }

        //    }

        //    throw new Exception("couldn't find station by drones location");
        //}

        ///// <summary>
        ///// Update the station to have one less spot for charging (because we sent a drone to charg there)
        ///// </summary>
        ///// <param name="StationId"></param>
        ///// <param name="drone"></param>
        //public Station UpdateStationChargingSpots(int StationId)
        //{
        //    Station station = DataSource.Stations.Find(x => x.Id == StationId);
        //    station.ChargeSpots -= 1;
        //    return station;
        //}
        ///// <summary>
        ///// Method of applying drone power
        ///// </summary>
        ///// <returns>An array of the amount of power consumption of a drone for each situation</returns>
        //public double[] PowerConsumptionRequest()
        //{
        //    double[] result = {DataSource.Config.Light, DataSource.Config.Heavy,
        //        DataSource.Config.Medium, DataSource.Config.Heavy,
        //        DataSource.Config.ChargingRate };
        //    return result;
        //}
        //public void updateBatteryDrone(int id, double dis)
        //{
        //    Drone d = DataSource.Drones.Find(x => x.Id == id);
        //    DataSource.Drones.Remove(d);
        //    d.Battery -= dis * 0.01;
        //    DataSource.Drones.Add(d);
        //}
        //#endregion

    }
}