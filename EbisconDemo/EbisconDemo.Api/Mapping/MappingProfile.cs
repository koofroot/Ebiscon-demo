using AutoMapper;
using EbisconDemo.Api.Models;
using EbisconDemo.Services.Models;

namespace EbisconDemo.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LoginRequestDto, LoginApiModel>()                
                .ReverseMap();

            CreateMap<RegistrationDto, RegistrationApiModel>()
                .ReverseMap();
        }
    }
}
