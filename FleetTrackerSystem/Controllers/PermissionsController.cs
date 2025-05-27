using FleetTrackerSystem.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FleetTrackerSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PermissionsController> _logger;
        public PermissionsController(IUnitOfWork unitOfWork, ILogger<PermissionsController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        [HttpGet("GetAllPermissions")]
        public async Task<IActionResult> GetAllPermissions()
        {
            var permissions = await _unitOfWork.Permission.GetAllPermissionsAsync();
            return Ok(permissions);
        }

        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetUserPermissions(string userId)
        {
            var userPermissions = await _unitOfWork.Permission.GetUserPermissionsAsync(userId);
            return Ok(userPermissions);
        }


        [HttpPost("users/{userId}")]
        public async Task<IActionResult> AssignPermissions(string userId, [FromBody] List<string> permissionNames)
        {
            if (permissionNames == null || !permissionNames.Any())
                return BadRequest("Permission list cannot be empty.");

            await _unitOfWork.Permission.AssignPermissionsAsync(userId, permissionNames);
            return Ok("Permissions assigned successfully.");
        }


        [HttpDelete("users/{userId}")]
        public async Task<IActionResult> RevokePermissions(string userId)
        {
            await _unitOfWork.Permission.RevokePermissionsAsync(userId);
            return Ok("Permissions revoked successfully.");
        }





    }
}
