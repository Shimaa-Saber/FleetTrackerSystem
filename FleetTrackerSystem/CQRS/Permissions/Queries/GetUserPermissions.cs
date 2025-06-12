using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.Repositories.Interfaces;
using MediatR;

namespace FleetTrackerSystem.CQRS.Permissions.Queries
{
    public class GetUserPermissions : IRequest<IEnumerable<Permission>>
    {
        public string UserId { get; set; }
        public GetUserPermissions(string userId)
        {
            UserId = userId;
        }
    }
    public class GetUserPermissionsHandler : IRequestHandler<GetUserPermissions, IEnumerable<Permission>>
    {
        private readonly IPermissionRepository _permissionRepository;
        public GetUserPermissionsHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
        public async Task<IEnumerable<Permission>> Handle(GetUserPermissions request, CancellationToken cancellationToken)
        {
            var userPermissionNames = await _permissionRepository.GetUserPermissionsAsync(request.UserId);
            return userPermissionNames.Select(permissionName => new Permission { Name = permissionName });
        }
    }
}
