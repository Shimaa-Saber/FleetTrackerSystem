using FleetTrackerSystem.Domain.Interfaces;
using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.Infrastructure.Data;

namespace FleetTrackerSystem.Infrastructure.Repositories.Repos
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

