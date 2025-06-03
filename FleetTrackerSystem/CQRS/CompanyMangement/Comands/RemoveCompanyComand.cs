using FleetTrackerSystem.UnitOfWork;
using MediatR;

namespace FleetTrackerSystem.CQRS.CompanyMangement.Comands
{
    public class RemoveCompanyComand:IRequest
    {
        public int id;
    }


   public class RemoveComanyComandHandler : IRequestHandler<RemoveCompanyComand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RemoveComanyComandHandler(
            IUnitOfWork unitOfWork
            ) {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(RemoveCompanyComand request,CancellationToken cancellationToken)
        {
            //var company=_unitOfWork.Company.GetByID(request.id);
            _unitOfWork.Company.Remove(request.id);
           await _unitOfWork.SaveChangesAsync();
           
        }

    }
}
