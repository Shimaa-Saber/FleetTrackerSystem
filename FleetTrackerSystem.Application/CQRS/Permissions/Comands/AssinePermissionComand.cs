using FleetTrackerSystem.Domain.Interfaces;
using MediatR;

namespace FleetTrackerSystem.Application.CQRS.Permissions.Comands
{
    public class AssinePermissionComand : IRequest
    {
        public string UserId { get; set; }
        public List<string> PermissionNames { get; set; }

    }
    public class AssinePermissionComandHandler : IRequestHandler<AssinePermissionComand>
    {
        private readonly IPermissionRepository _permissionRepository;
        public AssinePermissionComandHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
        public async Task Handle(AssinePermissionComand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId) || request.PermissionNames == null || !request.PermissionNames.Any())
            {
                throw new ArgumentException("UserId and PermissionNames cannot be null or empty.");
            }
            await _permissionRepository.AssignPermissionsAsync(request.UserId, request.PermissionNames);
        }


    }
}
