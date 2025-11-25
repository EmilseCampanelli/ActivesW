using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Enums;
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
    public class GetCartsQueryHandler : IRequestHandler<GetCartsQuery, CartDto>
    {
        private readonly IRepository<Orden> _repository;
        private readonly IMapper _mapper;
        private readonly IPromotionEngine _promotionEngine;


        public GetCartsQueryHandler(IRepository<Orden> repository, IMapper mapper, IPromotionEngine promotionEngine)
        {
            _repository = repository;
            _mapper = mapper;
            _promotionEngine = promotionEngine;
        }

        public async Task<CartDto> Handle(GetCartsQuery request, CancellationToken cancellationToken)
        {
            var orden = await _repository
        .GetFiltered(o => o.OrdenState == OrdenState.PendienteCompra && o.UserId == request.UserId)
        .Include(o => o.ProductLine)
            .ThenInclude(pl => pl.Product)
                .ThenInclude(p => p.ProductImages)
        .FirstOrDefaultAsync(cancellationToken);

            if (orden == null)
                return null;

            var cart = new CartDto
            {
                Id = orden.Id,
                UserId = request.UserId,
                OrdenState = orden.OrdenState,
                ProductLine = new List<ProductLineDto>()
            };

            foreach (var line in orden.ProductLine)
            {
                var product = line.Product;

                // Todas las promociones aplicables a este producto
                var promos = _promotionEngine.GetPromotionsForProduct(product);

                // Precio final usando cantidad
                var result = _promotionEngine.CalculateFinalPrice(product, line.Amount);

                // Armar Dto de línea
                var lineDto = new ProductLineDto
                {
                    Id = line.Id,
                    Amount = line.Amount,
                    Price = line.Price,                // PRECIO DE LISTA
                    PriceFinal = (double)result.FinalPrice,           // PRECIO DESPUÉS DE PROMOS
                    ProductId = product.Id,
                    ProductTitle = product.Title,
                    ProductDescription = product.Description,
                    ImagesUrl = product.ProductImages?
                        .OrderBy(i => i.Orden)
                        .Select(pi => new ProductImageDto
                        {
                            Id = pi.Id,
                            Url = pi.Url,
                            Orden = pi.Orden
                        })
                        .ToList()
                };

                cart.ProductLine.Add(lineDto);
            }

            return cart;
        }
    }
}
