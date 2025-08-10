using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Entities
{
    public class ProductImage : BaseEntity
    {
        public string Url { get; set; }
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }
        public int Orden {  get; set; }
    }
}
