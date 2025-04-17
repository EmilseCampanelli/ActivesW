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

        public async Task<Cuenta> Add(Cuenta item)
        {
            _context.Cuentas.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Cuenta> Update(Cuenta item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }

        public async Task<Cuenta> Get(int id)
        {
            return await _context.Cuentas.FindAsync(id);
        }

        public Usuario GetByEmail(string email)
        {
            return _context.Usuarios
                .Include(u => u.Cuenta)
                .Include(u => u.Rol)
                .Include(u => u.Empresa)
                .Include(u => u.UsuarioTipo)
                .Include(u => u.Domicilios)
                .FirstOrDefault(u => u.Email == email);
        }

        public async Task<Cuenta> GetUserByEmailAsync(string username)
        {
            return await _context.Cuentas.FirstOrDefaultAsync(u => u.Email == username);
        }

        public async Task<bool> ValidatePasswordAsync(Cuenta user, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }

        public Usuario GetCollaboratorByIdUser(int id)
        {
            return _context.Usuarios
                .Include(u => u.Cuenta)
                .Include(u => u.Rol)
                .Include(u => u.Empresa)
                .Include(u => u.UsuarioTipo)
                .Include(u => u.Domicilios)
                .FirstOrDefault(u => u.CuentaId == id);
        }

        public List<Rol> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public async Task<Cuenta> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Cuentas
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }
    }
}
