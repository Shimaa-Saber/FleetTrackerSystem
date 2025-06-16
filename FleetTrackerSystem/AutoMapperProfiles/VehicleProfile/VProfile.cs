using AutoMapper;
using FleetTrackerSystem.Application.DTOS.Vehicles;
using FleetTrackerSystem.Domain.Models;

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

