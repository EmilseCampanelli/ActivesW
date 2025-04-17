using APIAUTH.Aplication.CQRS.Commands.Usuario.CreateUser;
using APIAUTH.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Usuario.UpdateUser
{
    public record UpdateUserCommand(
        int Id,
        string Nombre,
        string Apellido,
        int Documento,
        TipoDocumento TipoDocumento,
        string Telefono,
        string Email,
        string BackupEmail,
        Sexo Sexo,
        int? EmpresaId,
        int RolId,
        int? UsuarioTipoId
    ) : IRequest<bool>;

}
