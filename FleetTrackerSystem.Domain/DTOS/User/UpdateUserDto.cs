using System.ComponentModel.DataAnnotations;

namespace FleetTrackerSystem.Application.DTOS.User
{
    public class UpdateUserDto
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Role { get; set; }

        [Required]
        public int CompanyId { get; set; }
    }
}
