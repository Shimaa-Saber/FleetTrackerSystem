using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetTrackerSystem.Domain.Interfaces
{
    public interface IVehicleNotifier
    {
        Task NotifyVehicleAddedAsync(string vehicleName);
    }
}
