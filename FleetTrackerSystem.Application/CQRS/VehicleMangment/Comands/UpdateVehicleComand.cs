using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.Infrastructure.Repositories.Repos;
using FleetTrackerSystem.Infrastructure.UnitOfWork;
using MediatR;

namespace FleetTrackerSystem.Application.CQRS.VehicleMangment.Comands
{
    public class UpdateVehicleComand:IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }

    }
    public class UpdateVehicleComandHandler : IRequestHandler<UpdateVehicleComand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateVehicleComandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(UpdateVehicleComand request, CancellationToken cancellationToken)
        {
            var vehicle = _unitOfWork.Vehicle.GetByID(request.Id);
            if (vehicle == null)
            {
                throw new KeyNotFoundException($"Vehicle with ID {request.Id} not found.");
            }
            request.Map<Vehicle>();
            _unitOfWork.Vehicle.Update(vehicle);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
