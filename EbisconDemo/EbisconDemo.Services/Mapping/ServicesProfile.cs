using AutoMapper;
using EbisconDemo.Data.Models;
using EbisconDemo.Data.Models.Enums;
using EbisconDemo.Services.Models;

namespace EbisconDemo.Services.Mapping
{
    public class ServicesProfile : Profile
    {
        public ServicesProfile() 
        {
            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Rating, RatingDto>()
                .ReverseMap();

            CreateMap<User, UserDto>()
                .ReverseMap();

            CreateMap<RegistrationDto, User>()
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(_ => UserType.Customer));

            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<CreateOrderDto, Order>();

            CreateMap<NotificationDto, Notification>()
                .ReverseMap();
        }
    }
}
