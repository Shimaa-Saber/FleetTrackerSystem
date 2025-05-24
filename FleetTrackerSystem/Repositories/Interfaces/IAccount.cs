using FleetTrackerSystem.DTOS.User;

namespace FleetTrackerSystem.Repositories.Interfaces
{
    public interface IAccount
    {
        public Task<object> LoginUserAsync(LoginDto loginDto);
    }
}
