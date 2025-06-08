using APIAUTH.Data.Context;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace APIAUTH.Data.Repositorios
{
    public class MenuRepository : IMenuRepository
    {
        private readonly ActivesWContext _context;

        public MenuRepository(ActivesWContext context)
        {
            _context = context;
        }

        public async Task<List<Menu>> GetMenusAsync()
        {
            return await _context.Menus
                .Include(m => m.Children)
                .ToListAsync();
        }
    }
}
