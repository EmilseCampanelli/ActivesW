using APIAUTH.Domain.Enums;
using MediatR;

namespace APIAUTH.Aplication.CQRS.Commands.Usuario.CreateUser
{
    public record CreateUserCommand(
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
        int? UsuarioTipoId,
        CreateDomicilioCommand Domicilio
    ) : IRequest<int>; // Retorna el ID del nuevo usuario

    public class CreateDomicilioCommand
    {
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Ciudad { get; set; }
        public int ProvinciaId { get; set; }
        public int PaisId { get; set; }
        public string CodigoPostal { get; set; }
    }
}
