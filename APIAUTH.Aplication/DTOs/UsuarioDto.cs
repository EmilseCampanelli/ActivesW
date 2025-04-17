using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.DTOs
{
    public class UsuarioDto : BaseEntityDto
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

        public int? UsuarioTipoId { get; set; }
        public UsuarioTipoDto UsuarioTipoDto { get; set; }


        public int? EmpresaId { get; set; }
        public EmpresaDto Empresa { get; set; }

        public int CuentaId { get; set; }
        public CuentaDto? Cuenta { get; set; }

        public int RolId   { get; set; }
        public RoleDto? Rol { get; set; }

        public List<DomicilioDto> Domicilios { get; set; }


    }
}
