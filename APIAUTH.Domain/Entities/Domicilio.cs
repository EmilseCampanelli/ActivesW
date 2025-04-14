using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Entities
{
    public class Domicilio : BaseEntity
    {
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Ciudad { get; set; }
        public int ProvinciaId { get; set; }
        public virtual Provincia Provincia { get; set; }
        public int PaisId { get; set; }
        public virtual Pais Pais {get; set;}
        public string CodigoPostal { get; set; }

        public int UsuarioId {  get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
