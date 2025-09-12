namespace APIAUTH.Domain.Entities
{
    public class PromotionSegment : BaseEntity
    {
        public long PromotionId { get; set; }
        public Promotion Promotion { get; set; } = default!;

        public long CustomerSegmentId { get; set; }
        public CustomerSegment CustomerSegment { get; set; } = default!;
    }
}
