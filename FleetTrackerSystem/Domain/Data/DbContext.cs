using FleetTrackerSystem.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FleetTrackerSystem.Domain.Data
{
    public class FeetTrackerDbContext : IdentityDbContext<ApplicationUser, ApplicationRole,string>
    {
        public FeetTrackerDbContext()
        {
        }

        public FeetTrackerDbContext(DbContextOptions<FeetTrackerDbContext> options) : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; } 
        public DbSet<Company> Companies { get; set; } 
       
    }

}
