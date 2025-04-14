using APIAUTH.Aplication.DTOs;
using APIAUTH.Domain.Enums;
using MediatR;

namespace APIAUTH.Aplication.CQRS.Commands.Usuario.CreateUser
{
    public record CreateUserCommand(
        string Name,
        string LastName,
        int DocumentNumber,
        TipoDocumento DocumentType,
        string NumberPhone,
        string Email,
        string BackupEmail,
        Sexo Sexo,
        int? CompanyId,
        int RoleId,
        int? UserTypeId,
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
