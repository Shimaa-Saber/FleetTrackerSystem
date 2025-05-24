using FleetTrackerSystem.Repositories.Interfaces;

namespace FleetTrackerSystem.UnitOfWork
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        ICompany Company { get; }
        IVehicle Vehicle { get; }
        IUser User { get; }
        IAccount account { get; }
        Task SaveChangesAsync();
    }
    
    }

