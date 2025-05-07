using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Description { get; set; }
        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }

        public List<Category> SubCategory { get; set; } = new List<Category>();
    }
}
