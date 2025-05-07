using APIAUTH.Aplication.CQRS.Commands.Usuario.CreateUser;
using APIAUTH.Aplication.CQRS.Commands.Usuario.UpdateUser;
using APIAUTH.Aplication.DTOs;
using APIAUTH.Domain.Entities;
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

            CreateMap<Role, RoleDto>().ReverseMap();

            CreateMap<Company, CompanyDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
