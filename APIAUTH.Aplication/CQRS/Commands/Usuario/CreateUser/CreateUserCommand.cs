using APIAUTH.Aplication.DTOs;
using APIAUTH.Domain.Enums;
using MediatR;

namespace APIAUTH.Aplication.CQRS.Commands.Usuario.CreateUser
{
    public record CreateUserCommand(
        string Name,
        string LastName,
        string Email,
        string Password
    ) : IRequest<AuthDto>; // Retorna el ID del nuevo usuario
   
}
