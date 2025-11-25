using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Repository;

namespace APIAUTH.Aplication.Services.Implementacion
{
    public class PromotionEngine : IPromotionEngine
    {
        private readonly IRepository<Promotion> _promotionRepo;

        public PromotionEngine(IRepository<Promotion> promotionRepo)
        {
            _promotionRepo = promotionRepo;
        }

        public List<Promotion> GetPromotionsForProduct(Product product)
        {
            var now = DateTime.UtcNow;

            var promotions = _promotionRepo.GetAll()
                .Where(p =>
                    p.Active &&
                    (!p.StartsAtUtc.HasValue || p.StartsAtUtc <= now) &&
                    (!p.EndsAtUtc.HasValue || p.EndsAtUtc >= now)
                )
                .ToList();

            return promotions
                .Where(p => p.AppliesToProduct(product.Id, product.CategoryId))
                .ToList();
        }


        public PromotionResult CalculateFinalPrice(Product product, int quantity)
        {
            decimal basePrice = Convert.ToDecimal(product.Price);
            var promos = GetPromotionsForProduct(product);

            var result = new PromotionResult
            {
                PromotionsUsed = new List<Promotion>(),
                FinalPrice = basePrice
            };

            if (!promos.Any())
                return result;

            // Filtrar promos por cantidades mínimas
            promos = promos.Where(p =>
                (!p.MinQuantityRequired.HasValue || quantity >= p.MinQuantityRequired.Value) &&
                (!p.WholesaleQuantity.HasValue || quantity >= p.WholesaleQuantity.Value)
            ).ToList();

            if (!promos.Any())
                return result;

            var stackable = promos.Where(p => p.Stackable).ToList();
            var nonStackable = promos.Where(p => !p.Stackable).ToList();

            // Precio usando acumulables
            decimal priceStackable = basePrice;
            List<Promotion> usedStackable = new();

            foreach (var promo in stackable)
            {
                priceStackable = promo.ApplyToPrice(priceStackable);
                usedStackable.Add(promo);
            }

            // Precios usando promos no acumulables
            var nonStackablePrices = new List<(Promotion promo, decimal price)>();

            foreach (var promo in nonStackable)
            {
                decimal price = promo.WholesalePrice ?? promo.ApplyToPrice(basePrice);
                nonStackablePrices.Add((promo, price));
            }

            // Mejor promo no acumulable
            decimal bestNonStackablePrice = basePrice;
            Promotion? bestNonStackablePromo = null;

            if (nonStackablePrices.Any())
            {
                var best = nonStackablePrices.OrderBy(p => p.price).First();
                bestNonStackablePrice = best.price;
                bestNonStackablePromo = best.promo;
            }

            // Elección final
            if (bestNonStackablePromo != null && bestNonStackablePrice < priceStackable)
            {
                result.FinalPrice = bestNonStackablePrice;
                result.PromotionsUsed = new List<Promotion> { bestNonStackablePromo };
            }
            else
            {
                result.FinalPrice = priceStackable;
                result.PromotionsUsed = usedStackable;
            }

            return result;
        }


        public List<Promotion> FilterPromotionsForProduct(Product product, List<Promotion> allPromos)
        {
            // 1) Promos que aplican por producto o por categoría o global
            return allPromos
                .Where(p =>
                       p.AppliesToAllProducts ||
                       p.Products.Any(pp => pp.ProductId == product.Id) ||
                       p.Categories.Any(pc => pc.CategoryId == product.CategoryId))
                .ToList();
        }


    }

}
