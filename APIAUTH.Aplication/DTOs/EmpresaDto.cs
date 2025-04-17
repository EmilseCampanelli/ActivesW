using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.DTOs
{
    public class EmpresaDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Descripcion { get; set; }
        public string CUIT { get; set; }
        public string Direccion { get; set; }
        public DateTime OperationDate { get; set; }
    }
}
