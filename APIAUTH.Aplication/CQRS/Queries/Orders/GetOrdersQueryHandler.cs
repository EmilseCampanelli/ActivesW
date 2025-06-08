using APIAUTH.Aplication.CQRS.Queries.Users;
using APIAUTH.Aplication.DTOs;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Repository;
using APIAUTH.Shared.Response;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Queries.Orders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, PagedResponse<OrdenDto>>
    {
        private readonly IListRepository<Orden> _repository;
        private readonly IRepository<Orden> _readOnlyRepo;
        private readonly IMapper _mapper;

        public GetOrdersQueryHandler(IListRepository<Orden> repository, IRepository<Orden> readOnlyRepo, IMapper mapper)
        {
            _repository = repository;
            _readOnlyRepo = readOnlyRepo;
            _mapper = mapper;
        }


        public async Task<PagedResponse<OrdenDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var query = _readOnlyRepo.GetFiltered(p => p.OrdenState != Domain.Enums.OrdenState.PendienteCompra && p.UserId == request.UserId);

            return await _repository.GetPagedResultAsync(
                query,
                request.Parameters,
                p => _mapper.Map<OrdenDto>(p)
            );
        }
    }
}
