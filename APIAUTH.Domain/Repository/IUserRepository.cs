using APIAUTH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Repository
{
    public interface IUserRepository
    {
        Task<Cuenta> Add(Cuenta item);
        Task<Cuenta> Update(Cuenta item);
        Task<Cuenta> Get(int id);
        Usuario GetByEmail(string email);
        Task<Cuenta> GetUserByEmailAsync(string username);
        Task<bool> ValidatePasswordAsync(Cuenta user, string password);
        Usuario GetCollaboratorByIdUser(int id);
        List<Role> GetRoles();
        Task<Cuenta> GetUserByRefreshTokenAsync(string refreshToken);
    }
}
