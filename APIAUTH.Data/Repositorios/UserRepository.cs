using APIAUTH.Data.Context;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace APIAUTH.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ActivesWContext _context;

        public UserRepository(ActivesWContext context)
        {
            _context = context;
        }

        public async Task<Account> Add(Account item)
        {
            _context.Accounts.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Account> Update(Account item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }

        public async Task<Account> Get(int id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        public User GetByEmail(string email)
        {
            return _context.Users
                .Include(u => u.Account)
                .Include(u => u.Role)
                .Include(u => u.Company)
                .Include(u => u.Address)
                .FirstOrDefault(u => u.Email == email);
        }

        public async Task<Account> GetUserByEmailAsync(string username)
        {
            return await _context.Accounts.FirstOrDefaultAsync(u => u.Email == username);
        }

        public async Task<bool> ValidatePasswordAsync(Account user, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }

        public User GetCollaboratorByIdUser(int id)
        {
            return _context.Users
                .Include(u => u.Account)
                .Include(u => u.Role)
                .Include(u => u.Company)
                .Include(u => u.Address)
                .FirstOrDefault(u => u.AccountId == id);
        }

        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public async Task<Account> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }
    }
}
