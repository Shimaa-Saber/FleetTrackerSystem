using FleetTrackerSystem.DTOS.User;
using FleetTrackerSystem.Repositories.Repos;
using FleetTrackerSystem.UnitOfWork;
using MediatR;

namespace FleetTrackerSystem.CQRS.UserMangement.Queries
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
