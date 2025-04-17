using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Entities
{
    public class Favorito : BaseEntity
    {
        public int ProductoId { get; set; }
        public int UsuarioId { get; set; }
        public virtual Producto Producto { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
