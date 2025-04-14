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
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<EmpresaDto, Empresa>()
                .ReverseMap();
        }
    }
}
