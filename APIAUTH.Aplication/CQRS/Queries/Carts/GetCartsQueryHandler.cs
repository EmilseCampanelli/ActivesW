using APIAUTH.Aplication.DTOs;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Repository;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Queries.Carts
{
    public class GetCartsQueryHandler : IRequestHandler<GetCartsQuery, List<OrdenDto>>
    {
        private readonly IRepository<Orden> _repository;
        private readonly IMapper _mapper;


        public GetCartsQueryHandler(IRepository<Orden> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<OrdenDto>> Handle(GetCartsQuery request, CancellationToken cancellationToken)
        {
            var query = _repository
                .GetFiltered(p => p.OrdenState == Domain.Enums.OrdenState.PendienteCompra);

            var data = await query.ToListAsync(cancellationToken);

            return _mapper.Map<List<OrdenDto>>(data);
        }
    }
}
