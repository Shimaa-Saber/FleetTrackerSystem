using FleetTrackerSystem.Application.DTOS;
using FleetTrackerSystem.Application.DTOS.User;
using FleetTrackerSystem.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace FleetTrackerSystem.API.Controllers
{
    [EnableRateLimiting("FixedPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AccountController> _logger;
        public AccountController(
            IUnitOfWork unitOfWork,
            ILogger<AccountController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        [HttpPost("login")]
        public async Task<ActionResult<ResponseDto>> Login(LoginDto login)
        {
            if (ModelState.IsValid)
            {
                object data = await _unitOfWork.account.LoginUserAsync(login);
                if (data == null)
                {
                    return ResponseDto.Fail("Invalid email or passowrd");
                }
                return ResponseDto.Success(data, "Login successful");
            }
            return ResponseDto.Fail(ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList(), "errors");
        }

    }
}
