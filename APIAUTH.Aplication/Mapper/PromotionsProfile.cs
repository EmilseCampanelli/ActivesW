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
    public class PromotionsProfile : Profile
    {
        public PromotionsProfile()
        {
            CreateMap<Promotion, PromotionDto>()
                .ForMember(dest => dest.ProductIds,
                    opt => opt.MapFrom(src => src.Products.Select(p => p.ProductId)))
                .ForMember(dest => dest.CategoryIds,
                    opt => opt.MapFrom(src => src.Categories.Select(c => c.CategoryId)))
                .ReverseMap();

        }
    }
}
