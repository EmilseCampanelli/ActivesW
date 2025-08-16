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

namespace APIAUTH.Aplication.CQRS.Queries.Favorites
{
    public class GetFavoritesByUserHandler : IRequestHandler<GetFavoritesByUserQuery, List<ProductDto>>
    {
        private readonly IRepository<Favorite> _favRepo;
        private readonly IRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public GetFavoritesByUserHandler(
            IRepository<Favorite> favRepo,
            IRepository<Product> productRepo,
            IMapper mapper)
        {
            _favRepo = favRepo;
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> Handle(GetFavoritesByUserQuery request, CancellationToken cancellationToken)
        {
            var productIds = await _favRepo
                .GetFiltered(f => f.UserId == request.UserId)
                .Select(f => f.ProductId)
                .ToListAsync(cancellationToken);

            var products = await _productRepo
                .GetFiltered(p => productIds.Contains(p.Id))
                .ToListAsync(cancellationToken);

            
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
