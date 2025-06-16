

using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleetTrackerSystem.Domain.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; } 


        public string Address { get; set; }
        [ForeignKey("Company")]
        public int? CompanyId { get; set; } 
        public Company Company { get; set; }

        public ICollection<UserPermission>? UserPermissions { get; set; }
    }
}
