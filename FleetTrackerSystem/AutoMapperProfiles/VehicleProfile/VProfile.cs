using AutoMapper;
using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.DTOS.Vehicles;

namespace FleetTrackerSystem.AutoMapperProfiles.VehicleProfile
{
    public class VProfile: Profile
    {
        public VProfile()
        {
            CreateMap<Vehicle, AddVehicleDto>().ReverseMap();
            CreateMap<Vehicle, EditVehicleDto>().ReverseMap();
            //CreateMap<Vehicle, VehicleUpdateDTO>().ReverseMap();
        }
    }
    
    }

