using FleetTrackerSystem.Domain.Data;
using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.Repositories.Interfaces;

namespace FleetTrackerSystem.Repositories.Repos
{
    public class VehicleRepository:GenericRepository<Vehicle>, IVehicle
    {
        FeetTrackerDbContext _context;
        public VehicleRepository(FeetTrackerDbContext context) : base(context)
        {
            _context = context;
        }
    }
    
    }

