using FleetTrackerSystem.CQRS.VehicleMangment.Comands;
using FleetTrackerSystem.CQRS.VehicleMangment.Queries;
using FleetTrackerSystem.Domain.Enums;
using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.DTOS.Vehicles;
using FleetTrackerSystem.Repositories.Repos;
using FleetTrackerSystem.UnitOfWork;
using FleetTrackerSystem.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FleetTrackerSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleMangementController : ControllerBase
    {
      private readonly IMediator _mediator;
        public VehicleMangementController( 
             IMediator mediator)
            
        {
            _mediator = mediator;
           
        }
        [HttpGet]
        public async Task<ResponseViewModel<IEnumerable<Vehicle>>> GetAllVehicles()
        {
          var Vehicles= await _mediator.Send(new GetAllVehiclesQuery());
            return ResponseViewModel<IEnumerable<Vehicle>>.Success(Vehicles);
        }


        [HttpGet("{id}")]
        public async Task<ResponseViewModel<Vehicle>> GetVehicleById(int id)
        {
   
            var vehicle = await _mediator.Send(new GetVehiclesByIdQuery(id));
            return ResponseViewModel<Vehicle>.Success(vehicle);
        }


        [HttpPost("AddVehicle")]
        public async Task<IActionResult> AddVehicle(AddVehicleDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var vehicle = dto.Map<AddVehicleComand>();
           await _mediator.Send(vehicle);

            return NoContent();

        }

        [HttpPut("UpdateVehicle/{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, EditVehicleDto dto)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
           var vehicle = dto.Map<UpdateVehicleComand>();
             await  _mediator.Send(vehicle);
            return NoContent();
        }

        [HttpDelete("DeleteVehicle/{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID provided.");
            }
          
            await _mediator.Send(new RemoveVehicleComand()
            {
                Id = id
            });
            return NoContent();
        }
    }
}
