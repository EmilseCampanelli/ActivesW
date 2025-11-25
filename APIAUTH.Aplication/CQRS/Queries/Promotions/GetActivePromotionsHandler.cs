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
    public class GetActivePromotionsHandler :
    IRequestHandler<GetActivePromotionsQuery, List<PromotionDto>>
    {
        private readonly IRepository<Promotion> _repo;
        private readonly IMapper _mapper;

        public GetActivePromotionsHandler(IRepository<Promotion> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<PromotionDto>> Handle(GetActivePromotionsQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;

            var promos =  _repo.GetFiltered(p =>
                p.Active 
            ).ToList();

            return _mapper.Map<List<PromotionDto>>(promos);
        }
    }

}
