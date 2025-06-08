using APIAUTH.Aplication.DTOs;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Repository;
using APIAUTH.Shared.Response;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace APIAUTH.Aplication.CQRS.Queries.Products
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PagedResponse<ProductDto>>
    {
        private readonly IListRepository<Product> _repository;
        private readonly IRepository<Product> _readOnlyRepo;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IListRepository<Product> repository, IRepository<Product> readOnlyRepo, IMapper mapper)
        {
            _repository = repository;
            _readOnlyRepo = readOnlyRepo;
            _mapper = mapper;
        }

        public async Task<PagedResponse<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var query = _readOnlyRepo.GetAll()
        .Include(p => p.Category)
        .Select(p => new Product
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            Price = p.Price,
            Stock = p.Stock,
            ImagesUrl = p.ImagesUrl,
            CategoryId = p.CategoryId,
            Category = p.Category,
            Slug = p.Slug,
            ProductState = p.ProductState,
            Sizes = p.Sizes,
            Tags = p.Tags,
            Gender = p.Gender,
            IsFavorite = p.Favorites.Any(f => f.UserId == request.Parameters.UserId)
        });

            return await _repository.GetPagedResultAsync(
                query,
                request.Parameters,
                p =>  _mapper.Map<ProductDto>(p)
            );
        }
    }
}
