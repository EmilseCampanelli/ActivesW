using APIAUTH.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Stock {  get; set; }
        [NotMapped]
        public string ImagesUrl { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string Slug { get; set; }
        public ProductState ProductState { get; set; }
        public string Sizes { get; set; }
        public Gender Gender { get; set; }
        public string Tags { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        [NotMapped]
        public bool IsFavorite { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        [NotMapped]
        public List<Promotion> Promotions { get; set; }
        [NotMapped]
        public decimal PriceFinal { get; set; }

    }
}
