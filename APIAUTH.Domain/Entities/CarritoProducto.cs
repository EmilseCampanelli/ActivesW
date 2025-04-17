using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Entities
{
    public class CarritoProducto : BaseEntity
    {
        public int CarritoId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUnitario { get; set; }

        public virtual Carrito Carrito { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
