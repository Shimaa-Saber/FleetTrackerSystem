using FleetTrackerSystem.Domain.Models;

namespace FleetTrackerSystem.Domain.Interfaces
{
    public interface ICompany : IGeneric<Company>
    {
        bool Exists(int id);
    }
}
