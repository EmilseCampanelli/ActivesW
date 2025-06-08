using APIAUTH.Domain.Enums;

namespace APIAUTH.Domain.Entities
{
    public class Orden : BaseEntity
    {
        public int UserId { get; set; }
        public OrdenState OrdenState { get; set; }
        public DateTime OrdenDate {  get; set; }

        public virtual User User { get; set; }
        public List<ProductLine> ProductLine { get; set; }
    }
}
