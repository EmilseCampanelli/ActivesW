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
        string Name,
        string LastName,
        int DocumentNumber,
        TipoDocumento DocumentType,
        string NumberPhone,
        string Email,
        string BackupEmail,
        Sexo Sexo,
        int? CompanyId,
        int UserId,
        int RoleId,
        int? UserTypeId
    ) : IRequest<bool>;

}
