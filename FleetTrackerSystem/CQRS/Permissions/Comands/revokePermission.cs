using FleetTrackerSystem.Repositories.Interfaces;
using MediatR;

namespace FleetTrackerSystem.CQRS.Permissions.Comands
{
    public class revokePermission : IRequest
    {
        public string UserId { get; set; }

    }
    public class revokePermissionHandler : IRequestHandler<revokePermission>
    {
        private readonly IPermissionRepository _permissionRepository;
        public revokePermissionHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
        public async Task Handle(revokePermission request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
            {
                throw new ArgumentException("UserId cannot be null or empty.");
            }
            await _permissionRepository.RevokePermissionsAsync(request.UserId);
        }

    }
}

