namespace FleetTrackerSystem.Domain.Models
{
    public class UserPermission
    {

        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
