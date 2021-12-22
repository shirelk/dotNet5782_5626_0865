﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Math;
using DalObject;
using DalApi;
using DO;

namespace DalObject
{
    public partial class DalObject
    {
        /// <summary>
        /// add parcel to the parcels list
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public void AddParcel(Parcel p)
        {
            if (DataSource.Parcels.Exists(parcel => parcel.Id == p.Id))
            {
                throw new ParcelException($"ID {p.Id} already exists!!");
            }
            else
                DataSource.Parcels.Add(p);
        }
        /// <summary>
        /// view function for Parcel with id
        /// </summary>
        /// <param name="id"></param>
        public Parcel GetParcel(int id)
        {
            if (!DataSource.Parcels.Exists(item => item.Id == id))
            {
                throw new ParcelException($"ID: {id} does not exist!!");
            };
            return DataSource.Parcels.First(c => c.Id == id);
        }
        /// <summary>
        /// view lists functions for Parcel
        /// </summary>
        public IEnumerable<Parcel> ShowParcelList(Func<Parcel, bool> predicate = null)
        {
            if (predicate == null)
            {
                List<Parcel> ParcelList = new();
                foreach (Parcel element in DataSource.Parcels)
                {
                    ParcelList.Add(element);
                }
                return ParcelList;
            }
            return DataSource.Parcels.Where(predicate).ToList();
        }
        /// <summary>
        /// shows the list of packages that haven't been associated to a drone
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Parcel> ShowNonAssociatedParcelList()
        {
            List<Parcel> NonAssociatedParcelList = new();
            foreach (Parcel element in DataSource.Parcels)
            {
                if (element.DroneID == 0)
                    NonAssociatedParcelList.Add(element);
            }
            return NonAssociatedParcelList;
        }
        /// <summary>
        /// update function: parcel to drone by id
        /// </summary>
        /// <param name="parcel_id"></param>
        /// <param name="drone_id"></param>
        public int UpdateParcelToDrone(int parcel_id, int drone_id)
        {
            Parcel parcel = default;
            try
            {
                parcel = GetParcel(parcel_id); 
            }
            catch (ParcelException pex)
            {
                throw new ParcelException($"Couldn't attribute drone {drone_id} to parcel", pex);
            }

            Drone drone = DataSource.Drones.Find(x => x.Id == drone_id); //finds the drone by its ID
            if (drone.Id == 0)
            {
                throw new DroneException($"noo drone found");
            }
            parcel.DroneID = drone.Id;
            parcel.Assigned = DateTime.Now;
            return parcel.Id;
            
            
        }
        /// <summary>
        /// Update function for parcel
        /// </summary>
        /// <param name="parcel_id"></param>
        /// <param name="drone_id"></param>
        public void UpdateParcelPickedupByDrone(int parcel_id, int drone_id)
        {
            Parcel p = DataSource.Parcels.Find(x => x.Id == parcel_id);
            Drone d = DataSource.Drones.Find(x => x.Id == drone_id);
            p.PickedUp = DateTime.Now;
            //d.Status = DroneStatuses.Shipping;
        }
        /// <summary>
        ///  Update parcel delivered to Customer
        /// </summary>
        /// <param name="parcel_id"></param>
        /// <param name="customer_id"></param>
        public void UpdateDeliveryToCustomer(int parcel_id, int customer_id)
        {
            Parcel p = DataSource.Parcels.Find(x => x.Id == parcel_id);
            p.Supplied = DateTime.Now;
        }
        /// <summary>
        /// Get Parce lBy DroneIds parcel
        /// </summary>
        /// <param name="DroneId"></param>
        /// <returns></returns>
        public Parcel GetParcelByDroneId(int DroneId)
        {
            Parcel p = DataSource.Parcels.Find(x => x.DroneID == DroneId);
            return p;
        }
        /// <summary>
        /// Search for the package in delivery mode
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        public  Parcel GetParcelInTransferByDroneId(int droneId)
        {
            Parcel p = DataSource.Parcels.Find(x => x.Id == droneId);
            return p;
        }


        public void DischargeDrone(int drone_id, double longt, double latit)
        {
            throw new NotImplementedException();
        }
        public List<Parcel> GetListOfParcelSending(int id)
        {
            List<Parcel> Listparcels = new();
            foreach (Parcel item in DataSource.Parcels)
            {
                if(item.SenderId==id)
                {
                    Listparcels.Add(item);
                }
            }
            return Listparcels;
        }
        public List<Parcel> GetListOfParcelRecirver(int id)
        {
            List<Parcel> Recieverparcels = new();
            foreach (Parcel item in DataSource.Parcels)
            {
                if (item.ReceiverId == id)
                {
                    Recieverparcels.Add(item);
                }
            }
            return Recieverparcels;
        }
        /// <summary>
        /// A function that calculates the distance between two points on the map
        /// </summary>
        /// <param name="senderId">sender Id</param>
        /// <param name="targetId">target Id</param>
        /// <returns>Returns a distance between two points</returns>
        public double GetDistanceBetweenLocationsOfParcels(int senderId, int targetId)
        {
            double minDistance = 1000000000000;
            Customer sender = GetCustomer(senderId);
            Customer target = GetCustomer(targetId);
            foreach (var s in DataSource.Stations)
            {
                double dictance = Math.Sqrt(Math.Pow(sender.Latitude - target.Latitude, 2) + Math.Pow(sender.Longitude - target.Longitude, 2));
                if (minDistance > dictance)
                {
                    minDistance = dictance;
                }
            }
            return minDistance;
        }
    }  
}