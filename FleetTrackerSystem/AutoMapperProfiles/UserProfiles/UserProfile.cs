using AutoMapper;
using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.DTOS.User;

namespace FleetTrackerSystem.AutoMapperProfiles.UserProfiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
            CreateMap<CreateUserDto, ApplicationUser>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());





            CreateMap<UpdateUserDto, ApplicationUser>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

        }
    }
    
    }

