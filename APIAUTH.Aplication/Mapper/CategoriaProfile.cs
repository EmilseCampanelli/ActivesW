using APIAUTH.Aplication.DTOs;
using APIAUTH.Domain.Entities;
using AutoMapper;

namespace APIAUTH.Aplication.Mapper
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<CategoriaDto, Categoria>()
                .ReverseMap();
        }
    }
}
