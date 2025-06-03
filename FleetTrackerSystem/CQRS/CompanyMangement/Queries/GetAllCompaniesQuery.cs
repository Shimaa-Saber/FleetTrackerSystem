using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.UnitOfWork;
using MediatR;

namespace FleetTrackerSystem.CQRS.CompanyMangement.Queries
{
    public class GetAllCompaniesQuery: IRequest<IEnumerable<Company>>
    {

    }

    public class GetAllComaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, IEnumerable<Company>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllComaniesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Company>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            var companies = _unitOfWork.Company.GetAll();
            return await Task.FromResult(companies);
        }
    }
}
