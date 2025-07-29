using APIAUTH.Domain.Enums;
using MediatR;

namespace APIAUTH.Aplication.CQRS.Commands.Usuario.UpdateUser
{
    public record UpdateUserCommand(
        int Id,
        string Name,
        string LastName,
        int Document,
        DocumentType DocumentType,
        string Phone,
        string Email,
        string BackupEmail,
        Gender Gender,
        int? CompanyId,
        int RoleId,
        string Image
    ) : IRequest<bool>;

}
