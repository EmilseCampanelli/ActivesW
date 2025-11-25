using APIAUTH.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.DTOs
{
    public class PromotionDto : BaseEntityDto
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

        // Regla de compra mínima
        public int? MinQuantityRequired { get; set; }

        // Regla de precio mayorista
        public int? WholesaleQuantity { get; set; }
        public decimal? WholesalePrice { get; set; }

        // Relaciones
        public List<int> ProductIds { get; set; }
        public List<int> CategoryIds { get; set; }
    }

}
