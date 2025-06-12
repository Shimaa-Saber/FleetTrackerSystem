using FleetTrackerSystem.UnitOfWork;
using MediatR;

namespace FleetTrackerSystem.CQRS.UserMangement.Comands
{
    public class RemoveUserComand : IRequest
    {
        public string Id { get; set; }
    }


    public class RemoveUserComandHandler : IRequestHandler<RemoveUserComand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RemoveUserComandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(RemoveUserComand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.User.GetUserById(request.Id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            await _unitOfWork.User.DeleteUser(request.Id);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}

