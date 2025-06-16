using FleetTrackerSystem.Application.DTOS.User;
using FleetTrackerSystem.Infrastructure.Repositories.Repos;
using FleetTrackerSystem.Infrastructure.UnitOfWork;
using MediatR;

namespace FleetTrackerSystem.Application.CQRS.UserMangement.Queries
{
    public class GetUserByEmailQuery : IRequest<UserDto>
    {
        public string Email { get; set; }
        public GetUserByEmailQuery(string email)
        {
            Email = email;
        }
    }
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetUserByEmailQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.User.GetUserByEmail(request.Email);
            return user.Map<UserDto>();
        }

    }
}

