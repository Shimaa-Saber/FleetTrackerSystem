

using FleetTrackerSystem.Application.DTOS.User;

namespace FleetTrackerSystem.Domain.Interfaces
{
    public interface IAccount
    {
        public Task<object> LoginUserAsync(LoginDto loginDto);
    }
}
