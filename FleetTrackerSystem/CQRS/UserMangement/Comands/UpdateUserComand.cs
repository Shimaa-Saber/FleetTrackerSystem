using FleetTrackerSystem.DTOS.User;
using FleetTrackerSystem.Repositories.Repos;
using FleetTrackerSystem.UnitOfWork;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FleetTrackerSystem.CQRS.UserMangement.Comands
{
    public class UpdateUserComand : IRequest
    {
        public string Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }
        [Required]

        public string Email { get; set; }
        [Required]

        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(50)]
        public string Address { get; set; }
    }

    public class UpdateUserComandHandler : IRequestHandler<UpdateUserComand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateUserComandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(UpdateUserComand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.User.GetUserById(request.Id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
        var user2=request.Map<UpdateUserDto>();
          await  _unitOfWork.User.UpdateUser(user2);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
    
    

