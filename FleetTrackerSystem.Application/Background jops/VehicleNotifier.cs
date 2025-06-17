using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FleetTrackerSystem.Domain.Interfaces;

namespace FleetTrackerSystem.Application.Background_jops
{
    public class VehicleNotifier: IVehicleNotifier
    {
        public Task NotifyVehicleAddedAsync(string vehicleName)
        {
            Console.WriteLine($"🔔 Notification: Vehicle {vehicleName} added.");
            return Task.CompletedTask;
        }
    }
}
