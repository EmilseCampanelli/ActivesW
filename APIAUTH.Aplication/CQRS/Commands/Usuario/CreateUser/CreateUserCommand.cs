using APIAUTH.Domain.Enums;
using MediatR;

namespace APIAUTH.Aplication.CQRS.Commands.Usuario.CreateUser
{
    public record CreateUserCommand(
        string Name,
        string LastName,
        string Email,
        Gender Gender,
        string Password
    ) : IRequest<int>; // Retorna el ID del nuevo usuario
   
}
