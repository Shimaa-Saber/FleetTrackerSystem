using Microsoft.AspNetCore.Identity;

namespace FleetTrackerSystem.Domain.Models
{
    public class ApplicationRole:IdentityRole<string>
    {
        public ApplicationRole(string roleName) : base(roleName)
        {
        }
        public ApplicationRole()
        {
        }
    }
}
