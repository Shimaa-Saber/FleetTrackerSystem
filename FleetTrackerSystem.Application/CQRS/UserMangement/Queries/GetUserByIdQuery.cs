using FleetTrackerSystem.Application.DTOS.User;
using FleetTrackerSystem.Infrastructure.Repositories.Repos;
using FleetTrackerSystem.Infrastructure.UnitOfWork;
using MediatR;

namespace FleetTrackerSystem.Application.CQRS.UserMangement.Queries
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public string Id { get; set; }
        public GetUserByIdQuery(string id)
        {
            Id = id;
        }
    }
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetUserByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.User.GetUserById(request.Id);
            return user.Map<UserDto>();
        }

    }
}

