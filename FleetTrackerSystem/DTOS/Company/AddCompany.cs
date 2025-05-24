using System.ComponentModel.DataAnnotations;

namespace FleetTrackerSystem.DTOS.Company
{
    public class AddCompany
    {
        [Required]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }   
        [Required]

        public string Email { get; set; }  
        [Required]
        
        public string PhoneNumber { get; set; }  
          [Required]
        [MaxLength(50)]  
        public string Address { get; set; }                
       
    }
}
