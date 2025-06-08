using APIAUTH.Aplication.DTOs;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Repository;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Queries.Orders
{
    public class GetOrdenByIdHandler : IRequestHandler<GetOrdenByIdQuery, OrdenDto>
    {
        private readonly IRepository<Orden> _ordenRepo;
        private readonly IMapper _mapper;

        public GetOrdenByIdHandler(IRepository<Orden> ordenRepo, IMapper mapper)
        {
            _ordenRepo = ordenRepo;
            _mapper = mapper;
        }

        public async Task<OrdenDto> Handle(GetOrdenByIdQuery request, CancellationToken cancellationToken)
        {
            var orden = await _ordenRepo.Get(request.Id);

            if (orden == null)
                throw new Exception("Orden no encontrada.");

            return _mapper.Map<OrdenDto>(orden);
        }
    }

}
