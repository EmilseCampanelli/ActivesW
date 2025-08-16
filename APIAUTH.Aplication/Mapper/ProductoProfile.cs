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
            CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => SplitSizes(src.Sizes)))
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => SplitSizes(src.Tags)))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Description : null))
            .ForMember(dest => dest.IsFavorite, opt => opt.MapFrom(src => src.IsFavorite))
            .ForMember(dest => dest.ProductImages, opt => opt.Ignore())
            .ForMember(dest => dest.ProductsImageDto, opt => opt.MapFrom(src => src.ProductImages))
            .ForMember(dest => dest.IsFavorite, opt => opt.MapFrom(src => src.Favorites.Any()))
            .ForMember(dest => dest.ImagesUrl, opt => opt.MapFrom(src => src.ProductImages.OrderBy(s => s.Orden).FirstOrDefault().Url))
            .ReverseMap()
            .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => JoinSizes(src.Sizes)))
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => JoinSizes(src.Tags)))
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.ProductImages, opt => opt.Ignore());


            CreateMap<CreateProductoCommand, ProductDto>();
            CreateMap<UpdateProductoCommand, ProductDto>();


            CreateMap<ProductImageDto, ProductImage>().ReverseMap();
        }

        private static string[] SplitSizes(string input) =>
         string.IsNullOrEmpty(input) ? Array.Empty<string>() : input.Split(',');

        private static string JoinSizes(string[] array) =>
            array != null ? string.Join(",", array) : null;
    }
}
