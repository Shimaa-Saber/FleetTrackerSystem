using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.DTOS.User;

namespace FleetTrackerSystem.Repositories.Interfaces
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

