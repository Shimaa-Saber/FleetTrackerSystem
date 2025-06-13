using FleetTrackerSystem.Domain.Data;
using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.DTOS.User;
using FleetTrackerSystem.Repositories.Interfaces;
using FleetTrackerSystem.Repositories.Repos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

internal class UserRepository : IUser
{
    private readonly FeetTrackerDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public UserRepository(FeetTrackerDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public async Task<UserDto> CreateUser(CreateUserDto userDto)
    {

        var user = userDto.Map<ApplicationUser>();

        var result = await _userManager.CreateAsync(user, userDto.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            throw new Exception(string.Join(", ", errors));
        }


        if (!string.IsNullOrEmpty(userDto.Role))
            await _userManager.AddToRoleAsync(user, userDto.Role);


        return user.Map<UserDto>();
    }

    public async Task<bool> DeleteUser(string id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;
        _context.Users.Remove(user);
        return await _context.SaveChangesAsync() > 0;
    }



    public async Task<IEnumerable<UserDto>> GetAllUsers()

    {
        var users = await _context.Users.ToListAsync();
        return users.Select(u => u.Map<UserDto>());
    }


    public async Task<UserDto> GetUserByCompanyId(int companyId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.CompanyId == companyId);
        return user?.Map<UserDto>();
    }

    public async Task<UserDto> GetUserById(string id)
    {
        var user = await _context.Users.FindAsync(id);
        return user?.Map<UserDto>();
    }
    public async Task<UserDto> GetUserByEmail(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user?.Map<UserDto>();
    }
    public async Task<bool> UserExists(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<UserDto> UpdateUser(UpdateUserDto userDto)
    {
        var user = await _context.Users.FindAsync(userDto.Id);
        if (user == null) return null;

        var User = userDto.Map<ApplicationUser>();

        _context.Users.Update(User);

        return user.Map<UserDto>();
    }
}



