using APIAUTH.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Promotion.Create
{
    public class CreatePromotionCommand : IRequest<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public PromotionType Type { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountValue { get; set; }

        public DateTime? StartsAtUtc { get; set; }
        public DateTime? EndsAtUtc { get; set; }

        public bool Active { get; set; }
        public bool Stackable { get; set; }
        public int Priority { get; set; }
        public bool AppliesToAllProducts { get; set; }

        public int? MinQuantityRequired { get; set; }
        public int? WholesaleQuantity { get; set; }
        public decimal? WholesalePrice { get; set; }

        public List<PromotionSegmentDto> Segments { get; set; }
        public List<PromotionProductDto> Products { get; set; }
        public List<PromotionCategoryDto> Categories { get; set; }
    }

    public class PromotionSegmentDto
    {
        public int CustomerSegmentId { get; set; }
    }

    public class PromotionProductDto
    {
        public int ProductId { get; set; }
    }

    public class PromotionCategoryDto
    {
        public int CategoryId { get; set; }
    }

}
