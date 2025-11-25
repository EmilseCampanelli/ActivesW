using APIAUTH.Domain.Enums;

namespace APIAUTH.Aplication.DTOs
{
    public class OrdenDto : BaseEntityDto
    {
        public int UserId { get; set; }
        public OrdenState OrdenState { get; set; }
        public DateTime OrdenDate { get; set; }
        public double Total { get; set; }

        public string UserName { get; set; }
        public List<ProductLineDto> ProductLine { get; set; }
    }
}
