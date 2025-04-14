using APIAUTH.Aplication.CQRS.Commands.Usuario.CreateUser;
using APIAUTH.Aplication.DTOs;
using Microsoft.AspNetCore.Http;

namespace APIAUTH.Aplication.Interfaces
{
    public interface IUsuarioService : IGenericService<UsuarioDto>
    {
        Task<string> PutImage(IFormFile image);
        Task<List<RoleDto>> GetRoles();
        Task Blocked(int id);
        Task<int> AddToUsuarioAsync(int usuarioId, CreateDomicilioCommand dto);
    }
}
