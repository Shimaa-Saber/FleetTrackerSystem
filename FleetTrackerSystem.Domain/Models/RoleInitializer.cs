using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FleetTrackerSystem.Domain.Models
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
