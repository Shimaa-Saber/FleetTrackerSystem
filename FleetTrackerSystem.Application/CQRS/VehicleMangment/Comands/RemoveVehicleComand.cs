using FleetTrackerSystem.Infrastructure.UnitOfWork;
using MediatR;

namespace FleetTrackerSystem.Application.CQRS.VehicleMangment.Comands
{
    public class RemoveVehicleComand : IRequest
    {
        public int Id { get; set; }
    }



    public class RemoveVehicleComandHandler : IRequestHandler<RemoveVehicleComand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RemoveVehicleComandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(RemoveVehicleComand request, CancellationToken cancellationToken)
        {
            var vehicle = _unitOfWork.Vehicle.GetByID(request.Id);
            if (vehicle == null)
            {
                throw new KeyNotFoundException($"Vehicle with ID {request.Id} not found.");
            }
            _unitOfWork.Vehicle.Remove(request.Id);
            await _unitOfWork.SaveChangesAsync();
        }




    }
}

