﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using DalObject.DO;
using IBL.BO;
using IDAL.DO;
using NuGet.Protocol.Plugins;

namespace IBL.BO
{
    public partial class BLObject
    {
        /// <summary>
        /// Add drone
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="stationId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public void AddDrone(Drone drone, int stationId)
        {
            //ID is less than 4 digits or more than 9 digits
            if (drone.Id < 1000 || drone.Id > 999999999)
            {
                throw new DronelIdException(drone.Id, "Drone ID must be between 4 to 9 digits");
            }
            //Weight category must be between 1-3
            if ((double)drone.Weight < 1 || (double)drone.Weight > 3)
            {
                throw new WeightCategoryException(drone.Weight, "Weight category must be between 1-3");
            }
            //station ID should be 5-6 digits
            if (stationId < 10000 || stationId >= 1000000)
            {
                throw new StationException(stationId, "Station ID should be 5 to 6 digits");
            }

            IDAL.DO.Drone d = new()
            {
                Id = drone.Id,
                Model = drone.Model,
                MaxWeight = (WeightCategories)drone.Weight,
                Battery = r.Next(20, 40),
                Status= IDAL.DO.DroneStatuses.Maintenance //when added a new drone it goes to initial charging
            };

            //get Station to update Location
            IDAL.DO.Station station = dalo.GetStation(stationId);
            drone.Battery = d.Battery;
            dalo.AddDrone(d); //adds the drone to the dal object
            AddDroneToList(drone, station.Latitude,station.Longitude);
            dalo.UpdateChargeSpots(station.Id);
            dalo.UpdateAddDroneToCharge(drone.Id, station.Id);
        }

        /// <summary>
        /// Update drone's name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        public void UpdateDroneName(int id, string model)
        {
            //if the recived ID does not exist
            if (!dronesL.Exists(item => item.Id == id))
            {
                throw new DronelIdException(id, $"Drone ID: {id} Does not exist!!");
            }
            //update BL drone to list 
            dronesL.Find(item => item.Id == id).Model = model;
            dalo.UpdateNameOfDrone(id, model);
        }

        /// <summary>
        /// Charging drone
        /// </summary>
        /// <param name="droneId"></param>
        public void UpdateChargeDrone(int droneId)
        {
            IDAL.DO.Station station = new();
            //finds the drone by the recived ID
            
            DroneToList dronel = dronesL.Find(x => x.Id == droneId);
            //if the drone is available- it can be sent for charging
            if (dronel.DroneStatuses == DroneStatuses.Available)
            {
                List<Distanse> disStationFromDrone = dalo.MinimumDistance(dronel.Location.Longitude, dronel.Location.Latitude);//list of the distances from the drone to every station
                double min = 9999999;
                int idS, counter = 0;
                bool flag = false;
                //number of distances in the list
                int sized = disStationFromDrone.Count;
                //goes over the list
                while (flag==false && counter <= sized)
                {
                    foreach (Distanse item in disStationFromDrone)
                    {
                        //to find the station with the minimum distance from the drone
                        if (item.Distance <= min)
                        {
                            min = item.Distance;
                            idS = item.Id;
                        }
                    
                        station = dalo.GetStation(item.Id);
                        //if there is an available charging spot in the station
                        if (station.ChargeSpots > 0)
                        {
                            //only if there is enough battery
                            if (dronel.Battery > min * 10 / 100)
                            {
                                flag = true;
                                //function to update Battery, drone mode drone location
                                UpdateDroneToStation(droneId, station.Id, min);
                            }
                        }
                        counter++;
                        disStationFromDrone.Remove(item);
                        if (flag)
                            break;
                    }
                }
                if (flag == false)
                {
                    throw new Exception("drone can not be sent for charging! ");
                }
            }
            else
            {
                throw new Exception("drone can not be sent for charging its is not Available ! ");
            }
        }

        /// <summary>
        /// Update drone to station
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="stationId"></param>
        /// <param name="minDistance"></param>
        public void UpdateDroneToStation(int droneId, int stationId, double minDistance)
        {
            //update for the way to the station
            //finds the drone by its ID
            DroneToList dronel = dronesL.Find(x => x.Id == droneId);
            IDAL.DO.Station station = new();
            //finds the station by its ID
            station = dalo.GetStation(stationId);
            //update the drone to charging status
            dronel.DroneStatuses = DroneStatuses.Maintenance;
            //update the drone's location to the charging station location - latitude and longitude
            dronel.Location.Latitude = station.Latitude;
            dronel.Location.Longitude = station.Longitude;
            double droneBattery = minDistance * 0.1;
            dronel.Battery = droneBattery;
            //עידכון עמדות טעינה פנוייות 
            dalo.UpdateChargeSpots(station.Id);
            //הוספת מופע לרשימת הרחפנים בטעינה
            dalo.UpdateAddDroneToCharge(droneId, station.Id);
        }

        /// <summary>
        /// Discharge drone
        /// </summary>
        /// <param name="droneID"></param>
        /// <param name="chargingTime"></param>
        /// <exception cref="Exception"></exception>
        public void DischargeDrone(int droneID, TimeSpan chargingTime)
        {
            // save dVal in second
            double dVal = (chargingTime.TotalMilliseconds) / 1000;

            //finds the drone by its ID
            DroneToList dronel = dronesL.Find(x => x.Id == droneID);
            IDAL.DO.Station station = new();
            //only a drone that was in charging c

            //ould be discharge
            if (dronel.DroneStatuses == DroneStatuses.Maintenance)
            {
                double droneLocationLatitude = dronel.Location.Latitude;
                double droneLocationLongitude = dronel.Location.Longitude;
                station = dalo.DischargeDroneByLocation(droneID, droneLocationLatitude, droneLocationLongitude);
                DroneInCharging droneInCharge = new();
                dronel.Battery += dVal * dalo.PowerRequest()[4];
                dronesL.Remove(dronel);
                dronel.DroneStatuses = DroneStatuses.Available;
                dronesL.Add(dronel);
                //remove the drone frome the list of droneChargings
                dalo.UpdateRemoveDroneToCharge(droneID, station.Id);
            }
            else
            {
                throw new Exception("drone can't be discharged");
            }
        }

        /// <summary>
        /// Get drone by ID
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Drone GetDrone(int droneId)
        {
            IDAL.DO.Drone d = dalo.GetDrone(droneId);
            Drone drone = new()
            {
                Id = d.Id,
                Model = d.Model,
                Battery = d.Battery,
                DroneStatuses = (DroneStatuses)d.Status,
                Weight = (Weight)d.MaxWeight
            };
            //to find the locations drone---
            DroneToList droneToList = dronesL.Find(x => x.Id == droneId);
            if (drone.DroneStatuses != DroneStatuses.Shipping)
            {
                return drone;
            }
            else
            {
                //Package data in transfer mode 
                IDAL.DO.Parcel parcel = dalo.GetParcelInTransferByDroneId(droneId);
                ParcelInTransfer parcelInTransfer = new()
                {
                    Id = parcel.Id,
                    Priority = (Priority)parcel.Priority,
                    Weight = (Weight)parcel.Weight,
                    ParcelTransferStatus = ParcelTransferStatus.OnTheWayToDestination
                };
                parcelInTransfer.Sender = new()
                {
                    Id = parcel.SenderId,
                };
                
               
                drone.ParcelInTransfer = parcelInTransfer;
            }
            return drone;
        }
        /// <summary>
        /// Show LIST of drones
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<DroneToList> ShowDroneList()
        {
            var droness = dalo.ShowDroneList();
            List<DroneToList> droneList = new();
            foreach (var item in droness)
            {
                DroneToList droneTL = new()
                {
                    Id = item.Id,
                    Model = item.Model,
                    Weight = (Weight)item.MaxWeight,
                    Battery = item.Battery,
                    DroneStatuses = (DroneStatuses)item.Status,
                };
                //to find the locations of the drone
                DroneToList droneToList = dronesL.Find(x => x.Id == item.Id);
                droneTL.Location = droneToList.Location;
                //finds the parcel in transfer ID
                Drone drone =GetDrone(item.Id);
                droneTL.ParcelNumberTransferred = drone.ParcelInTransfer.Id;
                droneList.Add(droneTL);
            }
            return droneList;
        }


        /// <summary>
        /// Imports the drone from the data layer and prints a drone from a logical entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

    }       
}
