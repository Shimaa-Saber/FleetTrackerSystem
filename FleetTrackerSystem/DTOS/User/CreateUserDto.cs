using System.ComponentModel.DataAnnotations;

namespace FleetTrackerSystem.DTOS.User
{
    public class CreateUserDto
    {
       
       
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; }

        [Required]
        public int CompanyId { get; set; }
    }
}
