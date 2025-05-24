using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.DTOS.User;
using FleetTrackerSystem.Repositories.Repos;
using FleetTrackerSystem.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FleetTrackerSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public UserController(
            ILogger<UserController> logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _unitOfWork.User.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid ID provided.");
            }
            var user = await _unitOfWork.User.GetUserById(id);
            return Ok(user);
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(CreateUserDto Dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

          var  createdUser= await _unitOfWork.User.CreateUser(Dto);
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);


        }

        [HttpPut("{id}/UpdateUser")]
        public IActionResult UpdateUser(string id,  UpdateUserDto Dto)
        {
            if (string.IsNullOrEmpty(id) || id != Dto.Id)
            {
                return BadRequest("Invalid ID provided.");
            }
            var user = _unitOfWork.User.GetUserById(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            _unitOfWork.User.UpdateUser(Dto);
            _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}/DeleteUser")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid ID provided.");
            }
            var user = _unitOfWork.User.GetUserById(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            await _unitOfWork.User.DeleteUser(id);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("GetUserByCompanyId/{companyId}")]
        public IActionResult GetUserByCompanyId(int companyId
            )
        {
            if (companyId <= 0)
            {
                return BadRequest("Invalid Company ID provided.");
            }
            var user = _unitOfWork.User.GetUserByCompanyId(companyId);
            return Ok(user);
        }

        [HttpGet("GetUserByEmail/{email}")]
        public IActionResult GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Invalid Email provided.");
            }
            var user = _unitOfWork.User.GetUserByEmail(email);
            return Ok(user);
        }

        [HttpGet("UserExists/{email}")]
        public IActionResult UserExists(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Invalid Email provided.");
            }
            var exists = _unitOfWork.User.UserExists(email);
            return Ok(exists);
        }

      
    }
}
