namespace FleetTrackerSystem.Domain.Models
{
    public class BaseClass
    {
        public int ID { get; set; }
        public bool ?IsDeleted { get; set; }
        public DateTime ?CreatedAt { get; set; }
        public int ?CreatedBy { get; set; }

    }
}
