using FleetTrackerSystem.Application.DTOS.User;
using FleetTrackerSystem.Infrastructure.Repositories.Repos;
using FleetTrackerSystem.Infrastructure.UnitOfWork;
using MediatR;

namespace FleetTrackerSystem.Application.CQRS.UserMangement.Queries
{
    public class GetAllUsersQuery: IRequest<IEnumerable<UserDto>>
    {
    }




    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IUnitOfWork _unitofwork;
        public GetAllUsersQueryHandler(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;

        }
        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitofwork.User.GetAllUsers();
            return users.Select(user => user.Map<UserDto>());

        }
    }
}
