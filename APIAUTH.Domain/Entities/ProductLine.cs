using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Entities
{
    public class ProductLine : BaseEntity
    {
        public int Amount { get; set; }
        public double Price { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int OrdenId { get; set; }
        public virtual Orden Orden { get; set; }
    }
}
