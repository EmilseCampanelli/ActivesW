using APIAUTH.Aplication.CQRS.Commands.Categoria.CreateCategoria;
using APIAUTH.Aplication.CQRS.Commands.Categoria.UpdateCategoria;
using APIAUTH.Aplication.DTOs;
using APIAUTH.Domain.Entities;
using AutoMapper;

namespace APIAUTH.Aplication.Mapper
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<CategoriaDto, Category>()
                .ReverseMap();

            CreateMap<CreateCategoriaCommand, CategoriaDto>();
            CreateMap<UpdateCategoriaCommand, CategoriaDto>();
        }
    }
}
