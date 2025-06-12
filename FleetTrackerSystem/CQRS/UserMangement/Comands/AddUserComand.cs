using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.DTOS.User;
using FleetTrackerSystem.Repositories.Repos;
using FleetTrackerSystem.UnitOfWork;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FleetTrackerSystem.CQRS.UserMangement.Comands
{
    public class AddUserComand : IRequest
    {
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; }

        [Required]
        public int CompanyId { get; set; }
    }
    public class AddUserComandHandler : IRequestHandler<AddUserComand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddUserComandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(AddUserComand request, CancellationToken cancellationToken)
        {
            var user = request.Map<CreateUserDto>();
            await _unitOfWork.User.CreateUser(user);
            await _unitOfWork.SaveChangesAsync();
           
        }

    }
}

