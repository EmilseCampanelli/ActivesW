using APIAUTH.Aplication.CQRS.Commands.Usuario.CreateUser;
using APIAUTH.Aplication.CQRS.Commands.Usuario.UpdateUser;
using APIAUTH.Aplication.DTOs;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Enums;
using AutoMapper;

namespace APIAUTH.Aplication.Mapper
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {

            CreateMap<UpdateUserCommand, UserDto>()
           .ForMember(dest => dest.Addresses, opt => opt.Ignore()); // 👈 muy importante



            CreateMap<UserDto, User>()
                .ReverseMap();

            CreateMap<User, UserGetDto>()
                .ForMember(dest => dest.RoleDescription, opt => opt.MapFrom(src => src.Role != null ? src.Role.Description : null))
                .ReverseMap()
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.Document, opt => opt.MapFrom(src => src.Document != 0 ? (int?)src.Document : null))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender != 0 ? (Gender?)src.Gender : null));

            CreateMap<Role, RoleDto>().ReverseMap();

            CreateMap<Company, CompanyDto>().ReverseMap();
            CreateMap<Address, AddressDto>()
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country != null ? src.Country.Description : null))
                .ForMember(dest => dest.ProvinceName, opt => opt.MapFrom(src => src.Province != null ? src.Province.Description : null))
                .ReverseMap()
                .ForMember(dest => dest.Country, opt => opt.Ignore())
                .ForMember(dest => dest.Province, opt => opt.Ignore());
        }
    }
}
