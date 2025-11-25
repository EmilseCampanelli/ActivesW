using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Interfaces;
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
        private readonly IPromotionEngine _promotionEngine;
        private readonly IRepository<Promotion> _promotionRepo;

        public GetProductsQueryHandler(IListRepository<Product> repository, IRepository<Product> readOnlyRepo, IMapper mapper, IPromotionEngine promotionEngine, IRepository<Promotion> promotionRepo)
        {
            _repository = repository;
            _readOnlyRepo = readOnlyRepo;
            _mapper = mapper;
            _promotionEngine = promotionEngine;
            _promotionRepo = promotionRepo;
        }

        public async Task<PagedResponse<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;

            var allPromos = await _promotionRepo.GetAll()
                        .Where(p => p.Active &&
                               (!p.StartsAtUtc.HasValue || p.StartsAtUtc <= now) &&
                               (!p.EndsAtUtc.HasValue || p.EndsAtUtc >= now))
                        .ToListAsync();

                            var query = _readOnlyRepo.GetAll()
                         .Include(p => p.Category)
                         .Include(p => p.ProductImages)
                         .Select(p => new Product
                                 {
                                     Id = p.Id,
                                     Title = p.Title,
                                     Description = p.Description,
                                     Price = p.Price,
                                     Stock = p.Stock,
                                     ImagesUrl = p.ProductImages.OrderBy(s => s.Orden).FirstOrDefault().Url,
                                     CategoryId = p.CategoryId,
                                     Category = p.Category,
                                     Slug = p.Slug,
                                     ProductState = p.ProductState,
                                     Sizes = p.Sizes,
                                     Tags = p.Tags,
                                     Gender = p.Gender,
                                     ProductImages = p.ProductImages,
                                     Favorites = p.Favorites
                                 });


            var paged = await _repository.GetPagedResultAsync(query, request.Parameters,
        p => {
            var dto = _mapper.Map<ProductDto>(p);

            // 4) Aplicar el motor de promociones EN MEMORIA
            var promos = _promotionEngine.FilterPromotionsForProduct(p, allPromos);
            dto.Promotions = promos.Select(pm => new PromotionSimpleDto
            {
                Id = pm.Id,
                Name = pm.Name,
                DiscountValue = pm.DiscountValue,
                DiscountType = pm.DiscountType
            }).ToList();

            dto.PriceFinal = _promotionEngine.CalculateFinalPrice(p, 1).FinalPrice;

            dto.IsFavorite = p.Favorites.Any(f => f.UserId == request.Parameters.UserId);

            return dto;
        });

            return paged;
        }
    }
}
