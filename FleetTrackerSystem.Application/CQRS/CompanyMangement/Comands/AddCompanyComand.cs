
using FleetTrackerSystem.Domain.Models;


using MediatR;
using System.ComponentModel.DataAnnotations;
using FleetTrackerSystem.Infrastructure.UnitOfWork;
using FleetTrackerSystem.Infrastructure.Repositories.Repos;

namespace FleetTrackerSystem.Application.CQRS.CompanyMangement.Comands
{
    public class AddCompanyComand : IRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }

    public class AddCompanyComandHandler : IRequestHandler<AddCompanyComand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddCompanyComandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(AddCompanyComand request, CancellationToken cancellationToken)
        {
            var company = request.Map<Company>();


            _unitOfWork.Company.Add(company);
            await _unitOfWork.SaveChangesAsync();
            
        }
    }
}
