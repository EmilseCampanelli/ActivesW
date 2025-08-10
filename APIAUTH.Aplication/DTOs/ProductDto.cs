using APIAUTH.Domain.Enums;
using System.Text.Json.Serialization;

namespace APIAUTH.Aplication.DTOs
{
    public class ProductDto : BaseEntityDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string ImagesUrl { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Slug { get; set; }
        public ProductState ProductState { get; set; }
        public bool IsFavorite { get; set; }
        public string[] Sizes { get; set; }
        public string[] Tags { get; set; }
        public Gender Gender { get; set; }

        [JsonIgnore]
        public List<ProductImageAddDto> ProductImages { get; set; }

        public List<ProductImageDto> ProductsImageDto { get; set; }
    }
}
