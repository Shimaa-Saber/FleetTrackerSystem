using AutoMapper;
using FleetTrackerSystem.DTOS.Company;
using FleetTrackerSystem.Domain.Models;

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
