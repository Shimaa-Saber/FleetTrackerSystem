using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.DTOS.User;
using FleetTrackerSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FleetTrackerSystem.Repositories.Repos
{
    public class AccountRepository : IAccount
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<object> LoginUserAsync(LoginDto loginDto)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return null;
            }
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result)
            {
                return null;
            }
            double exp = 1;

            string token = await CreateTokenAsync(user, exp);
            IList<string> roles = await _userManager.GetRolesAsync(user);
            return new { Token = token, UserId = user.Id, ExpDate = DateTime.UtcNow.AddHours(exp), Roles = roles };
        }

        public async Task<IdentityResult> AddUserToRoleAsync(string userName, string role)
        {
            ApplicationUser? user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "User not found."
                });
            }
            if (!await _roleManager.RoleExistsAsync(role))
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "RoleNotFound",
                    Description = "Role not found."
                });
            }
            IdentityResult result = await _userManager.AddToRoleAsync(user, role);
            return result;
        }

        public async Task<bool> IsUserExist(string userName)
        {
            ApplicationUser? user = await _userManager.FindByNameAsync(userName);
            return user != null;
        }

        private async Task<string> CreateTokenAsync(ApplicationUser user, double exp = 1)
        {
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                expires: DateTime.UtcNow.AddHours(exp),
                claims: await GetUserClaimsAsync(user),
                signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<IEnumerable<Claim>> GetUserClaimsAsync(ApplicationUser user)
        {
            List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email)
                };
            IList<string> roles = await _userManager.GetRolesAsync(user);
            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        //public async Task<object> LoginAsync(LoginDto loginDto)
        //{
        //    return await LoginUserAsync(loginDto);
        //}
    }
    

    }

