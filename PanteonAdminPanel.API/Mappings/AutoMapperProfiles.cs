using AutoMapper;
using PanteonAdminPanel.API.DTO;
using PanteonAdminPanel.API.DTO.ConfigurationDTO;
using PanteonAdminPanel.API.Models;

namespace PanteonAdminPanel.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Configuration, ConfigurationDto>().ReverseMap();
            CreateMap<Configuration, CreateConfigurationDto>().ReverseMap();
            CreateMap<Configuration, UpdateConfigurationDto>().ReverseMap();
        }
    }
}