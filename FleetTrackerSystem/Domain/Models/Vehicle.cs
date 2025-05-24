using System.ComponentModel.DataAnnotations.Schema;

namespace FleetTrackerSystem.Domain.Models
{
    public class Vehicle: BaseClass
    {
       

        [ForeignKey("Company")]
        public int? CompanyId { get; set; }            
        public string Name { get; set; }                    
        public string Type { get; set; }                  
        public string Color { get; set; }

        public Company? Company { get; set; }
    }
}
