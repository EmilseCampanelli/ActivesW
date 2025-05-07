using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Entities
{
    public class Favorite : BaseEntity
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
