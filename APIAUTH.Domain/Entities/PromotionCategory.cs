namespace APIAUTH.Domain.Entities
{
    public class PromotionCategory : BaseEntity
    {
        public int PromotionId { get; set; }
        public Promotion Promotion { get; set; } = default!;

        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
    }
}
