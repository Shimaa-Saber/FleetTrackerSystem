using FleetTrackerSystem.Domain.Data;
using FleetTrackerSystem.Repositories.Interfaces;

namespace FleetTrackerSystem.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FeetTrackerDbContext _context;
        public ICompany Company { get; private set; }

        public IVehicle Vehicle { get; private set; }

        public IUser User { get; private set; }
        public IAccount account { get; private set; }
        public UnitOfWork(
            FeetTrackerDbContext context,
            ICompany company,
            IVehicle vehicle,
            IUser user,
            IAccount account)
            
        {   _context = context;
            Company = company;
            Vehicle = vehicle;
            User = user;
            this.account = account;
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
