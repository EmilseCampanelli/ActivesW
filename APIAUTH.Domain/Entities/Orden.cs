using APIAUTH.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Entities
{
    public class Orden : BaseEntity
    {
        public int UserId { get; set; }
        public OrdenState OrdenState { get; set; }
        public double Total {  get; set; }
        public DateTime OrdenDate {  get; set; }

        public virtual User User { get; set; }
        public List<ProductLine> ProductLine { get; set; }
    }
}
