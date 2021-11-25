﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
   public class Station
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public int AvailableChargingSpots { get; set; } //Number of available charging spots in free
        public List<DroneInCharging> droneInChargings= new List<DroneInCharging>(); //list of drones in charging

        public override string ToString()
        {
            String result = "";
            result += $"station ID is {Id}, \n";
            result += $"name station is: {Name}, \n";
            result += $"num of AvailableChargingSpots is {Location}, \n";
            result += $"list of droneInChargings {droneInChargings}, \n";
            return result;
        }

    }
   
}

