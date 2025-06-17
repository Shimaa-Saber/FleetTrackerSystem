using System.ComponentModel.DataAnnotations;

namespace FleetTrackerSystem.Application.DTOS.Vehicles
{
    public class AddVehicleDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Color { get; set; }
    }
}
