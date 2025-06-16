using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.Infrastructure.UnitOfWork;
using MediatR;

namespace FleetTrackerSystem.Application.CQRS.CompanyMangement.Queries
{
    public class GetCompanyByIdQuery : IRequest<Company>
    {
        public int Id { get; set; }
        public GetCompanyByIdQuery(int id)
        {
            Id = id;
        }
    }

    public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, Company>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetCompanyByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Company> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            
            return await Task.FromResult(_unitOfWork.Company.GetByID(request.Id));
        }
    }
}

