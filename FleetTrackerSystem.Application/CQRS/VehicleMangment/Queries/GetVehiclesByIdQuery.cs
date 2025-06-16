using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.Infrastructure.UnitOfWork;
using MediatR;

namespace FleetTrackerSystem.Application.CQRS.VehicleMangment.Queries
{
    public class GetVehiclesByIdQuery : IRequest<Vehicle>
    {
        public int Id { get; set; }
        public GetVehiclesByIdQuery(int id)
        {
            Id = id;
        }
    }



    public class GetVehiclesByIdQueryHandler : IRequestHandler<GetVehiclesByIdQuery, Vehicle>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetVehiclesByIdQueryHandler(IUnitOfWork unitOfWork) { 
            _unitOfWork = unitOfWork;
        
        }

        public async Task<Vehicle> Handle(GetVehiclesByIdQuery request, CancellationToken cancellationToken)
        {
            var company = _unitOfWork.Vehicle.GetByID(request.Id);
            return await Task.FromResult(company);
            
        }
    }

}

