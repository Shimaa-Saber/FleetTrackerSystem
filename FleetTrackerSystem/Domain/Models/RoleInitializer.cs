using FleetTrackerSystem.Domain.Models;

using Microsoft.AspNetCore.Identity;

namespace Project_Api.Models
{
    public class RoleInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleNames = { "SuperAdmin", "RegularUser","CompanyAdmin"};

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new ApplicationRole(roleName));
                }
            }

          
        }
    }
}
