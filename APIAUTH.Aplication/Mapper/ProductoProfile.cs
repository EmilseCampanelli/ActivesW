using APIAUTH.Aplication.CQRS.Commands.Producto.CreateProducto;
using APIAUTH.Aplication.CQRS.Commands.Producto.UpdateProducto;
using APIAUTH.Aplication.DTOs;
using APIAUTH.Domain.Entities;
using AutoMapper;

namespace APIAUTH.Aplication.Mapper
{
    public class ProductoProfile : Profile
    {
        public ProductoProfile()
        {
            CreateMap<Product, ProductoDto>().ReverseMap();

            CreateMap<CreateProductoCommand, ProductoDto>();
            CreateMap<UpdateProductoCommand, ProductoDto>();
        }
    }
}
