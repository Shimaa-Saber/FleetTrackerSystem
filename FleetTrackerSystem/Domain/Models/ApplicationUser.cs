using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleetTrackerSystem.Domain.Models
{
    public class ApplicationUser:IdentityUser<string>
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; } 


        public string Address { get; set; }
        [ForeignKey("Company")]
        public int? CompanyId { get; set; } 
        public Company Company { get; set; }
    }
}
