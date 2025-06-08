namespace APIAUTH.Aplication.DTOs
{
    public class ProductLineDto : BaseEntityDto
    {
        public int Amount { get; set; }
        public double Price { get; set; }
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
    }
}
