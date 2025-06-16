using FleetTrackerSystem.Domain.Interfaces;
using FleetTrackerSystem.Infrastructure.Data;

namespace FleetTrackerSystem.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FeetTrackerDbContext _context;
        public ICompany Company { get; private set; }

        public IVehicle Vehicle { get; private set; }

        public IUser User { get; private set; }
        public IAccount account { get; private set; }
        public IPermissionRepository Permission { get; private set; }


        public UnitOfWork(
            FeetTrackerDbContext context,
            ICompany company,
            IVehicle vehicle,
            IUser user,
            IAccount account,
            IPermissionRepository permission
            )
            
        {   _context = context;
            Company = company;
            Vehicle = vehicle;
            User = user;
            this.account = account;
            Permission = permission;
        }




             
        
        
        
        

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task SaveChangesAsync()
        {
         await  _context.SaveChangesAsync();
        }
    }
}
