using APIAUTH.Domain.Enums;

namespace APIAUTH.Domain.Entities
{
    public class Usuario : BaseEntity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Documento { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        public string Telefono { get; set; }
        public string? PhotoUrl { get; set; }
        public string Email { get; set; }
        public string BackupEmail { get; set; }
        public Sexo Sexo { get; set; }

        public int UsuarioTipoId { get; set; }
        public virtual UsuarioTipo UsuarioTipo { get; set; }

        public virtual List<Domicilio>  Domicilios {get; set;}

        public int? EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }

        public int CuentaId { get; set; }
        public virtual Cuenta Cuenta { get; set; }

        public int RolId { get; set; }
        public virtual Rol Rol { get; set; }

    }
}
