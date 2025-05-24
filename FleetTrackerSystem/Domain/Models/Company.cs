namespace FleetTrackerSystem.Domain.Models
{
    public class Company :  BaseClass
    {
                     
        public string Name { get; set; }                   
        public string Email { get; set; }    
        
        public string PhoneNumber { get; set; }            
        public string Address { get; set; }                
        public DateTime ?CreatedAt { get; set; }

      public  ICollection<Vehicle> ?Vehicles { get; set; } = new List<Vehicle>();
        public ICollection<ApplicationUser> ?Drivers { get; set; } = new List<ApplicationUser>();
    }
}
