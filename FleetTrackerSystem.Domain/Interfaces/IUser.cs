using FleetTrackerSystem.Application.DTOS.User;
using FleetTrackerSystem.Domain.Models;

namespace FleetTrackerSystem.Domain.Interfaces
{
    public interface IUser
    {
        Task<UserDto> CreateUser(CreateUserDto userDto);
        Task<UserDto> UpdateUser(UpdateUserDto userDto);
        Task<bool> DeleteUser(string id);
        Task<UserDto> GetUserById(string id);

        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<UserDto> GetUserByEmail(string email);
       
        Task<UserDto> GetUserByCompanyId(int companyId);

        Task<bool> UserExists(string email);
     
       


    }
    
     


    }

