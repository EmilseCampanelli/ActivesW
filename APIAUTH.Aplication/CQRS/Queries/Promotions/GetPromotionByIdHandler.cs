using APIAUTH.Aplication.DTOs;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Enums;
using APIAUTH.Domain.Repository;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Queries.Promotions
{
    public class GetPromotionByIdHandler : IRequestHandler<GetPromotionByIdQuery, PromotionDto>
    {
        private readonly IRepository<Promotion> _repo;
        private readonly IMapper _mapper;

        public GetPromotionByIdHandler(IRepository<Promotion> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PromotionDto> Handle(GetPromotionByIdQuery request, CancellationToken cancellationToken)
        {
            var promo = await _repo.Get(request.Id);

            if (promo == null || promo.Status == BaseState.Eliminado)
                throw new Exception("Promoción no encontrada.");

            return _mapper.Map<PromotionDto>(promo);
        }
    }
}
