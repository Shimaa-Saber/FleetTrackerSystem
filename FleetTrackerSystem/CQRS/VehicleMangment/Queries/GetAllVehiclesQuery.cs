using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.UnitOfWork;
using MediatR;

namespace FleetTrackerSystem.CQRS.VehicleMangment.Queries
{
    public class GetAllVehiclesQuery:IRequest<IEnumerable<Vehicle>>
    {
    }



    public class GetAllVehiclesQueryHandler : IRequestHandler<GetAllVehiclesQuery, IEnumerable<Vehicle>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllVehiclesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Vehicle>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
        {
            var vehicles = _unitOfWork.Vehicle.GetAll();
            return await Task.FromResult(vehicles);

        }
    }
}



