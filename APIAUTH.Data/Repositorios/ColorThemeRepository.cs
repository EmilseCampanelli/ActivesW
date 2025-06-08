using APIAUTH.Data.Context;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace APIAUTH.Data.Repositorios
{
    public class ColorThemeRepository : IColorThemeRepository
    {
        private readonly ActivesWContext _db;

        public ColorThemeRepository(ActivesWContext db)
        {
            _db = db;
        }

        public async Task<List<ColorTheme>> GetAllAsync()
        {
            return await _db.ColorThemes.AsNoTracking().ToListAsync();
        }
    }
}
