using APIAUTH.Domain.Enums;
using MediatR;

namespace APIAUTH.Aplication.CQRS.Commands.Usuario.UpdateUser
{
    public record UpdateUserCommand(
        int Id,
        string Nombre,
        string Apellido,
        int Documento,
        DocumentType TipoDocumento,
        string Telefono,
        string Email,
        string BackupEmail,
        Gender Sexo,
        int? EmpresaId,
        int RolId,
        int? UsuarioTipoId
    ) : IRequest<bool>;

}
