using APIAUTH.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Entities
{
    public class Promotion : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; }

        public PromotionType Type { get; set; } = PromotionType.General;

        public DiscountType DiscountType { get; set; } = DiscountType.Percentage;

        public decimal DiscountValue { get; set; }

        public DateTime? StartsAtUtc { get; set; }
        public DateTime? EndsAtUtc { get; set; }

        public bool Active { get; set; } = true;

        public bool Stackable { get; set; } = false;

        public int Priority { get; set; } = 0;

        public bool AppliesToAllProducts { get; set; } = false;

        public List<PromotionSegment> Segments { get; set; } = new();


        public List<PromotionProduct> Products { get; set; } = new();

        public List<PromotionCategory> Categories { get; set; } = new();


        public bool IsActiveAt(DateTime utcNow)
        {
            if (!Active) return false;
            if (StartsAtUtc.HasValue && utcNow < StartsAtUtc.Value) return false;
            if (EndsAtUtc.HasValue && utcNow > EndsAtUtc.Value) return false;
            return true;
        }

        public bool AllowsSegment(CustomerSegment? segment)
        {
            if (Segments.Count == 0) return true; // applies to all segments
            if (segment is null) return false;
            return Segments.Any(s => s.CustomerSegmentId == segment.Id);
        }

        public bool AppliesToProduct(long productId, long? categoryId)
        {
            if (AppliesToAllProducts) return true;

            if (Products.Any(pp => pp.ProductId == productId))
                return true;

            if (categoryId.HasValue && Categories.Any(pc => pc.CategoryId == categoryId.Value))
                return true;

            return false;
        }

        public decimal ApplyToPrice(decimal basePrice)
        {
            if (DiscountType == DiscountType.Percentage)
            {
                var result = basePrice * (1 - (DiscountValue / 100m));
                return result < 0 ? 0 : Math.Round(result, 2);
            }
            else 
            {
                var result = basePrice - DiscountValue;
                return result < 0 ? 0 : Math.Round(result, 2);
            }
        }
    }
}
