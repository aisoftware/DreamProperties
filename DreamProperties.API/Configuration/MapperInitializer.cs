using AutoMapper;
using DreamProperties.API.Models;
using DreamProperties.Common.Models;

namespace DreamProperties.API.Configuration
{
    public class MapperInitializer: Profile
    {
        public MapperInitializer()
        {
            CreateMap<Property, PropertyDTO>().ReverseMap();
            CreateMap<UserDTO, AppUser>().ReverseMap();
        }
    }
}
