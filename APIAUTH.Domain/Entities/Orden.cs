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
        public int UsuarioId { get; set; }
        public int CarritoId { get; set; }
        public EstadoOrden EstadoOrden { get; set; }
        public double Total {  get; set; }
        public DateTime FechaCompra {  get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Carrito Carrito { get; set; }
    }
}
