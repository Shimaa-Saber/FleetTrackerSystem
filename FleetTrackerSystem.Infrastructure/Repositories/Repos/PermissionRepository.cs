using FleetTrackerSystem.Domain.Interfaces;
using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace FleetTrackerSystem.Infrastructure.Repositories.Repos
{
    public class PermissionRepository: IPermissionRepository
    {
        private readonly FeetTrackerDbContext _context;
       public UserManager<ApplicationUser> _userManager;


        public PermissionRepository(FeetTrackerDbContext context, UserManager<ApplicationUser> _userManager)
        {
            _context = context;
            this._userManager = _userManager;

        }

        public async Task<List<Permission>> GetAllPermissionsAsync()
        {
            return await _context.Permissions.ToListAsync();
        }

        public async Task<List<string>> GetUserPermissionsAsync(string userId)
        {
            return await _context.UserPermissions
                .Where(up => up.UserId == userId)
                .Select(up => up.Permission.Name)
                .ToListAsync();
        }

        public async Task AssignPermissionsAsync(string userId, List<string> permissionNames)
        {
            var permissions = await _context.Permissions
                .Where(p => permissionNames.Contains(p.Name))
                .ToListAsync();

            var userPermissions = permissions.Select(p => new UserPermission
            {
                UserId = userId,
                PermissionId = p.Id
            });

            await _context.UserPermissions.AddRangeAsync(userPermissions);
            await _context.SaveChangesAsync();
        }

        public async Task RevokePermissionsAsync(string userId)
        {
            //var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userPermissions = _context.UserPermissions.Where(up => up.UserId == userId);
            _context.UserPermissions.RemoveRange(userPermissions);
            await _context.SaveChangesAsync();
        }
    }
}
