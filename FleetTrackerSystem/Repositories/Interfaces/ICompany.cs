﻿using FleetTrackerSystem.Domain.Models;

namespace FleetTrackerSystem.Repositories.Interfaces
{
    public interface ICompany : IGeneric<Company>
    {
        bool Exists(int id);
    }
}
