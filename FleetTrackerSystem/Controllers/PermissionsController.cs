using FleetTrackerSystem.CQRS.CompanyMangement.Queries;
using FleetTrackerSystem.CQRS.Permissions.Comands;
using FleetTrackerSystem.CQRS.Permissions.Queries;
using FleetTrackerSystem.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FleetTrackerSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PermissionsController> _logger;
        public PermissionsController(IMediator mediator, ILogger<PermissionsController> logger)
        {
            
            _logger = logger;
            _mediator = mediator;
        }
        [HttpGet("GetAllPermissions")]
        public async Task<IActionResult> GetAllPermissions()
        {
          _logger.LogInformation("Fetching all permissions.");
            var permissions = await _mediator.Send(new GetAllCompaniesQuery());
            return Ok(permissions);
        }

        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetUserPermissions(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest("User ID cannot be null or empty.");
          

            var userPermissions = await _mediator.Send(new GetUserPermissions(userId));
            return Ok(userPermissions);
        }


        [HttpPost("users/{userId}")]
        public async Task<IActionResult> AssignPermissions(string userId, [FromBody] List<string> permissionNames)
        {
            if (permissionNames == null || !permissionNames.Any())
                return BadRequest("Permission list cannot be empty.");

            await _mediator.Send(new AssinePermissionComand
            {
                UserId = userId,
                PermissionNames = permissionNames
            });
            return Ok("Permissions assigned successfully.");
        }


        [HttpDelete("users/{userId}")]
        public async Task<IActionResult> RevokePermissions(string userId)
        {
           await _mediator.Send(new revokePermission
            {
                UserId = userId
            });
            return Ok("Permissions revoked successfully.");
        }





    }
}
