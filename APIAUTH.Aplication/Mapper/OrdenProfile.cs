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
    public class OrdenProfile : Profile
    {
        public OrdenProfile()
        {

            CreateMap<Orden, OrdenDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.Name : null))
                .ReverseMap();

            CreateMap<Orden, CartDto>()
                .ReverseMap();

            CreateMap<ProductLine, ProductLineDto>()
                .ForMember(dest => dest.ProductTitle, opt => opt.MapFrom(src => src.Product != null ? src.Product.Title : null))
                .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.Product != null ? src.Product.Description : null))
                .ForMember(dest => dest.ImagesUrl, opt => opt.MapFrom(src => src.Product != null ? src.Product.ProductImages : null))
                .ReverseMap()
                .ForMember(dest => dest.Product, opt => opt.Ignore());
        }
    }
}
