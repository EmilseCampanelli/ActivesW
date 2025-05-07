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
        Task<Account> Add(Account item);
        Task<Account> Update(Account item);
        Task<Account> Get(int id);
        User GetByEmail(string email);
        Task<Account> GetUserByEmailAsync(string username);
        Task<bool> ValidatePasswordAsync(Account user, string password);
        User GetCollaboratorByIdUser(int id);
        List<Role> GetRoles();
        Task<Account> GetUserByRefreshTokenAsync(string refreshToken);
    }
}
