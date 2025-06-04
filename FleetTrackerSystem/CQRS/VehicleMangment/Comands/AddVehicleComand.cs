using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.Repositories.Repos;
using FleetTrackerSystem.UnitOfWork;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FleetTrackerSystem.CQRS.VehicleMangment.Comands
{
    public class AddVehicleComand:IRequest
    {
        public string Name { get; set; }
       
        public string Type { get; set; }
     
        public string Color { get; set; }  

    }

    public class AddVehicleComandHandler: IRequestHandler<AddVehicleComand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddVehicleComandHandler(IUnitOfWork unitOfWork) { 
            _unitOfWork= unitOfWork;

        }

        public Task Handle(AddVehicleComand request, CancellationToken cancellationToken)
        {
            var company = request.Map<Vehicle>();
            _unitOfWork.Vehicle.Add(company);
            _unitOfWork.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }


}
