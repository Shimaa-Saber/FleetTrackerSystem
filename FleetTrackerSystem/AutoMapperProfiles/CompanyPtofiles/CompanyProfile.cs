using AutoMapper;
using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.Application.DTOS.Company;

namespace FleetTrackerSystem.AutoMapperProfiles.CompanyPtofiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, AddCompany>().ReverseMap();
            CreateMap<Company, UpdateCompanyDto>().ReverseMap();


        }
    }
}
