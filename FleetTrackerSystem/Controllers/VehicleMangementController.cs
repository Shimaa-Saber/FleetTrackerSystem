using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.DTOS.Vehicles;
using FleetTrackerSystem.Repositories.Repos;
using FleetTrackerSystem.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FleetTrackerSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleMangementController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public VehicleMangementController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetAllVehicles()
        {
            var vehicles = _unitOfWork.Vehicle.GetAll();
            return Ok(vehicles);
        }


        [HttpGet("{id}")]
        public IActionResult GetVehicleById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID provided.");
            }
            var vehicle = _unitOfWork.Vehicle.GetByID(id);
            return Ok(vehicle);
        }


        [HttpPost("AddVehicle")]
        public async Task<IActionResult> AddVehicle(AddVehicleDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var vehicle = dto.Map<Vehicle>();
            _unitOfWork.Vehicle.Add(vehicle);
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVehicleById), new { id = vehicle.ID }, dto);
        }

        [HttpPut("UpdateVehicle/{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, EditVehicleDto dto)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var vehicle = _unitOfWork.Vehicle.GetByID(id);
            if (vehicle == null)
            {
                return NotFound("Vehicle not found.");
            }
            vehicle = dto.Map<Vehicle>();
            _unitOfWork.Vehicle.Update(vehicle);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("DeleteVehicle/{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID provided.");
            }
            var vehicle = _unitOfWork.Vehicle.GetByID(id);
            if (vehicle == null)
            {
                return NotFound("Vehicle not found.");
            }
            _unitOfWork.Vehicle.Remove(id);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}
