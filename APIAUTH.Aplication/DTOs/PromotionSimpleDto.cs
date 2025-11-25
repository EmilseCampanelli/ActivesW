using APIAUTH.Domain.Enums;

namespace APIAUTH.Aplication.DTOs
{
    public class PromotionSimpleDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal DiscountValue { get; set; }
        public DiscountType DiscountType { get; set; }
    }
}
