using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace FleetTrackerSystem.Domain.Models
{
    public class ApplicationRole:IdentityRole
    {
        public string NormalizedName { get; set; }

        public ApplicationRole(string roleName) : base(roleName)
        {
        }
        public ApplicationRole()
        {
        }
    }
}
