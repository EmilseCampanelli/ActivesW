using APIAUTH.Aplication.CQRS.Commands.Usuario.CreateUser;
using APIAUTH.Aplication.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.Interfaces
{
    public interface IDomicilioService
    {
        Task<IEnumerable<DomicilioDto>> GetByUsuarioIdAsync(int usuarioId);
        Task<int> AddToUsuarioAsync(int usuarioId, CreateDomicilioCommand dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(DomicilioDto dto);
    }
}
