using APIAUTH.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Entities
{
    public class Carrito : BaseEntity
    {
        public int UsuarioId { get; set; }
        public EstadoCarrito EstadoCarrido { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual List<CarritoProducto> Productos { get; set; }
    }
}
