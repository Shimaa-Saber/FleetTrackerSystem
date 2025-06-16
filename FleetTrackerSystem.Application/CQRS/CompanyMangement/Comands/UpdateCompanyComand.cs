
using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.Infrastructure.Repositories.Repos;
using FleetTrackerSystem.Infrastructure.UnitOfWork;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FleetTrackerSystem.Application.CQRS.CompanyMangement.Comands
{

    public class UpdateCompanyComand:IRequest
    {
        public int Id { get; set; }
       
        public string Name { get; set; }
       

        public string Email { get; set; }
    

        public string PhoneNumber { get; set; }
      
        public string Address { get; set; }
    }

    public class UpdateCompanyQueryHandler : IRequestHandler<UpdateCompanyComand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCompanyQueryHandler(
            IUnitOfWork unitOfWork
            )
        {
            _unitOfWork = unitOfWork;

        }
        public async Task Handle(UpdateCompanyComand request, CancellationToken cancellationToken)
        {
            var company = request.Map<Company>();
            
            
            _unitOfWork.Company.Update(company);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
