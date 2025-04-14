using APIAUTH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.DTOs
{
    public class DomicilioDto : BaseEntityDto
    {
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Ciudad { get; set; }
        public int ProvinciaId { get; set; }
        public string ProvinciaName { get; set; }
        public int PaisId { get; set; }
        public string PaisName { get; set; }
        public string CodigoPostal { get; set; }
        public int UsuarioId { get; set; }

    }
}
