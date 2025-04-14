using APIAUTH.Aplication.CQRS.Commands.Usuario.CreateUser;
using APIAUTH.Aplication.CQRS.Commands.Usuario.UpdateUser;
using APIAUTH.Aplication.DTOs;
using APIAUTH.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.Mapper
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUserCommand, UsuarioDto>()
            .ForMember(dest => dest.Domicilios, opt => opt.MapFrom(src => new List<CreateDomicilioCommand> { src.Domicilio }));

            CreateMap<CreateDomicilioCommand, Domicilio>();
            CreateMap<CreateDomicilioCommand, DomicilioDto>();



            CreateMap<UpdateUserCommand, UsuarioDto>()
           .ForMember(dest => dest.Domicilios, opt => opt.Ignore()); // 👈 muy importante



            CreateMap<UsuarioDto, Usuario>()
                .ReverseMap();

            CreateMap<Role, RoleDto>().ReverseMap();

            CreateMap<Empresa, EmpresaDto>().ReverseMap();
            CreateMap<Domicilio, DomicilioDto>().ReverseMap();
        }
    }
}
