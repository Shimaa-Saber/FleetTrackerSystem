namespace FleetTrackerSystem.DTOS.User
{
    public class UserDto
    {

        public string Id { get; set; }

      
        public string? FirstName { get; set; } 
        public string? LastName { get; set; } 

        public string? Email { get; set; } 

        public string? PhoneNumber { get; set; } 

        public string ?Address { get; set; } 

        public int? CompanyId { get; set; }
        
        
        public string? Role { get; internal set; }
    }

}
