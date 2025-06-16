using FleetTrackerSystem.Domain.Interfaces;

namespace FleetTrackerSystem.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        ICompany Company { get; }
        IVehicle Vehicle { get; }
        IUser User { get; }
        IAccount account { get; }

        IPermissionRepository Permission { get; }
        Task SaveChangesAsync();
    }
    
    }

