using FleetTrackerSystem.Domain.Models;

namespace FleetTrackerSystem.Repositories.Interfaces
{
    public interface IPermissionRepository
    {
        Task<List<Permission>> GetAllPermissionsAsync();
        Task<List<string>> GetUserPermissionsAsync(string userId);
        Task AssignPermissionsAsync(string userId, List<string> permissionNames);
        Task RevokePermissionsAsync(string userId);
    }
}
