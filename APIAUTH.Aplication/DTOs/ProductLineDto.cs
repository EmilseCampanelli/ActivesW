namespace APIAUTH.Aplication.DTOs
{
    public class ProductLineDto : BaseEntityDto
    {
        public int Amount { get; set; }
        public double Price { get; set; }
        public double PriceFinal { get; set; }
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductDescription { get; set; }
        public List<ProductImageDto> ImagesUrl { get; set; }
        public string Size { get; set; }
        public string Slug { get; set; }
    }
}
