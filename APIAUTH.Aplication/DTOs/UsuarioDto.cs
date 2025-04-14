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
        public string Name { get; set; }
        public string LastName { get; set; }
        public int DocumentNumber { get; set; }
        public TipoDocumento DocumentType { get; set; }
        public string NumberPhone { get; set; }
        public string? Photo { get; set; }
        public string Email { get; set; }
        public string BackupEmail { get; set; }
        public Sexo Sexo { get; set; }

        public int? UserTypeId { get; set; }
        public UsuarioTipoDto UserTypeDto { get; set; }


        public int? CompanyId { get; set; }
        public EmpresaDto Company { get; set; }

        public int UserId { get; set; }
        public CuentaDto? User { get; set; }

        public int RoleId   { get; set; }
        public RoleDto? Role { get; set; }

        public List<DomicilioDto> Domicilios { get; set; }


    }
}
