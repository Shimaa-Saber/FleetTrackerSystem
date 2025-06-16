using FleetTrackerSystem.Domain.Interfaces;
using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.Infrastructure.Data;

namespace FleetTrackerSystem.Infrastructure.Repositories.Repos
{
    public class CompanyRepository:GenericRepository<Company>, ICompany
    {
       private readonly FeetTrackerDbContext _context;
        public CompanyRepository(FeetTrackerDbContext context) : base(context)
        {
            _context = context;
        }

        public bool Exists(int id)
        {
            return _context.Companies.Any(c => c.ID == id);

        }
    }
    
    }

