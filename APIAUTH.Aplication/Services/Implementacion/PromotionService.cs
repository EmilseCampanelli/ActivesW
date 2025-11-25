using APIAUTH.Aplication.CQRS.Commands.Promotion.Create;
using APIAUTH.Aplication.CQRS.Commands.Promotion.Update;
using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.Services.Implementacion
{
    public class PromotionService : IPromotionService
    {
        private readonly IRepository<Promotion> _repository;

        public PromotionService(IRepository<Promotion> repository)
        {
            _repository = repository;
        }

        public async Task<long> CreatePromotionAsync(CreatePromotionCommand cmd)
        {
            var entity = new Promotion
            {
                Name = cmd.Name,
                Description = cmd.Description,
                Type = cmd.Type,
                DiscountType = cmd.DiscountType,
                DiscountValue = cmd.DiscountValue,
                StartsAtUtc = cmd.StartsAtUtc,
                EndsAtUtc = cmd.EndsAtUtc,
                Active = cmd.Active,
                Stackable = cmd.Stackable,
                Priority = cmd.Priority,
                AppliesToAllProducts = cmd.AppliesToAllProducts,

                MinQuantityRequired = cmd.MinQuantityRequired,
                WholesaleQuantity = cmd.WholesaleQuantity,
                WholesalePrice = cmd.WholesalePrice,
            };

            entity.Segments = cmd.Segments
                .Select(s => new PromotionSegment { CustomerSegmentId = s.CustomerSegmentId })
                .ToList();

            entity.Products = cmd.Products
                .Select(p => new PromotionProduct { ProductId = p.ProductId })
                .ToList();

            entity.Categories = cmd.Categories
                .Select(c => new PromotionCategory { CategoryId = c.CategoryId })
                .ToList();

            await _repository.Add(entity);

            return entity.Id;
        }

        public async Task UpdatePromotionAsync(UpdatePromotionCommand cmd)
        {
            var entity = await _repository.Get(cmd.Id);

            if (entity == null)
                throw new Exception("Promotion not found");

            entity.Name = cmd.Name;
            entity.Description = cmd.Description;
            entity.Type = cmd.Type;
            entity.DiscountType = cmd.DiscountType;
            entity.DiscountValue = cmd.DiscountValue;
            entity.StartsAtUtc = cmd.StartsAtUtc;
            entity.EndsAtUtc = cmd.EndsAtUtc;
            entity.Active = cmd.Active;
            entity.Stackable = cmd.Stackable;
            entity.Priority = cmd.Priority;
            entity.AppliesToAllProducts = cmd.AppliesToAllProducts;

            entity.MinQuantityRequired = cmd.MinQuantityRequired;
            entity.WholesaleQuantity = cmd.WholesaleQuantity;
            entity.WholesalePrice = cmd.WholesalePrice;

            // Replace collections
            entity.Segments.Clear();
            entity.Segments.AddRange(
                cmd.Segments.Select(s => new PromotionSegment { CustomerSegmentId = s.CustomerSegmentId })
            );

            entity.Products.Clear();
            entity.Products.AddRange(
                cmd.Products.Select(p => new PromotionProduct { ProductId = p.ProductId })
            );

            entity.Categories.Clear();
            entity.Categories.AddRange(
                cmd.Categories.Select(c => new PromotionCategory { CategoryId = c.CategoryId })
            );

            await _repository.Update(entity);
        }
    }

}
