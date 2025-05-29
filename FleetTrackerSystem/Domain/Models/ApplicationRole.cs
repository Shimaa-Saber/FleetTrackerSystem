using Microsoft.AspNetCore.Identity;

namespace FleetTrackerSystem.Domain.Models
{
    public class ApplicationRole:IdentityRole
    {
        public ApplicationRole(string roleName) : base(roleName)
        {
        }
        public ApplicationRole()
        {
        }
    }
}
