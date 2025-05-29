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

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = "1", Name = "SuperAdmin", NormalizedName = "SUPERADMIN" },
                new ApplicationRole { Id = "2", Name = "RegularUser", NormalizedName = "REGULARUSER" },
                new ApplicationRole { Id = "3", Name = "CompanyAdmin", NormalizedName = "COMPANYADMIN" }
            );

        }





        }



    }
