using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.Repositories.Interfaces;
using MediatR;

namespace FleetTrackerSystem.CQRS.Permissions.Queries
{
    public class GetAllPermissionsQuery:IRequest<IEnumerable<Permission>>
    {
    }

    public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, IEnumerable<Permission>>
    {
        private readonly IPermissionRepository _permissionRepository;
        public GetAllPermissionsQueryHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
        public async Task<IEnumerable<Permission>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
        {
            return await _permissionRepository.GetAllPermissionsAsync();
        }
    }
}
