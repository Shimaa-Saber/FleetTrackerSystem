using FleetTrackerSystem.Application.CQRS.UserMangement.Comands;
using FleetTrackerSystem.Application.CQRS.UserMangement.Queries;
using FleetTrackerSystem.Application.DTOS.User;
using FleetTrackerSystem.Application.ViewModels;

using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.Infrastructure.Repositories.Repos;
using FleetTrackerSystem.Infrastructure.UnitOfWork;


using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.Tasks;

namespace FleetTrackerSystem.API.Controllers
{
    [EnableRateLimiting("FixedPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        public UserController(
            ILogger<UserController> logger,
            IMediator mediator,
            IUnitOfWork unitOfWork
            )
        {
            _logger = logger;
            _mediator = mediator;
            _unitOfWork = unitOfWork;

        }
        [HttpGet]
        public async Task<ResponseViewModel<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());

            return ResponseViewModel<IEnumerable<UserDto>>.Success(users);
        }

        [HttpGet("{id}")]
        public async Task<ResponseViewModel<UserDto>> GetUserById(string id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));
         return   ResponseViewModel<UserDto>.Success(user);

        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(CreateUserDto Dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var command = Dto.Map<AddUserComand>();
            await _mediator.Send(command);


            return NoContent();


        }

        [HttpPut("{id}/UpdateUser")]
        public async Task<IActionResult> UpdateUser(string id,  UpdateUserDto Dto)
        {
            if (string.IsNullOrEmpty(id) || id != Dto.Id)
            {
                return BadRequest("Invalid ID provided.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var command = Dto.Map<UpdateUserComand>();
           await _mediator.Send(command);


            return NoContent();
        }

        [HttpDelete("{id}/DeleteUser")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid ID provided.");
            }
           await _mediator.Send(new RemoveUserComand { Id = id });

            return NoContent();
        }



        [HttpGet("GetUserByCompanyId/{companyId}")]
        public async Task<ResponseViewModel<UserDto>> GetUserByCompanyId(int companyId)

        {
          
            var user = await _mediator.Send(new GetUserByComanyId(companyId));

            return ResponseViewModel<UserDto>.Success(user);
        }

        [HttpGet("GetUserByEmail/{email}")]
        public async Task<ResponseViewModel<UserDto>> GetUserByEmail(string email)
        {
            
            var user =await _mediator.Send(new GetUserByEmailQuery(email));
            return ResponseViewModel<UserDto>.Success(user);
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
