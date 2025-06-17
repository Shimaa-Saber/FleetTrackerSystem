using FleetTrackerSystem.Application.CQRS.CompanyMangement.Queries;
using FleetTrackerSystem.Application.CQRS.Permissions.Comands;
using FleetTrackerSystem.Application.CQRS.Permissions.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace FleetTrackerSystem.API.Controllers
{
    [EnableRateLimiting("FixedPolicy")]
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
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("GetAllPermissions")]
        public async Task<IActionResult> GetAllPermissions()
        {
          _logger.LogInformation("Fetching all permissions.");
            var permissions = await _mediator.Send(new GetAllCompaniesQuery());
            return Ok(permissions);
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetUserPermissions(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest("User ID cannot be null or empty.");
          

            var userPermissions = await _mediator.Send(new GetUserPermissions(userId));
            return Ok(userPermissions);
        }

        [Authorize(Roles = "SuperAdmin")]
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
        [Authorize(Roles = "SuperAdmin")]

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
