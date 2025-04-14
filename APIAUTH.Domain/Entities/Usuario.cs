using APIAUTH.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Entities
{
    public class Usuario : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int DocumentNumber { get; set; }
        public TipoDocumento DocumentType { get; set; }
        public string NumberPhone { get; set; }
        public string? Photo { get; set; }
        public string Email { get; set; }
        public string BackupEmail { get; set; }
        public Sexo Sexo { get; set; }

        public int UserTypeId { get; set; }
        public virtual UsuarioTipo UserType { get; set; }

        public virtual List<Domicilio>  Domicilios {get; set;}

        public int? CompanyId { get; set; }
        public virtual Empresa Company { get; set; }

        public int UserId { get; set; }
        public virtual Cuenta User { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

    }
}
