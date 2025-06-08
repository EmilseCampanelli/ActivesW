using APIAUTH.Domain.Entities;

namespace APIAUTH.Domain.Repository
{
    public interface IColorThemeRepository
    {
        Task<List<ColorTheme>> GetAllAsync();
    }

}
