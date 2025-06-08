using APIAUTH.Aplication.DTOs;

namespace APIAUTH.Aplication.Services.Interfaces
{
    public interface IMenuService
    {
        Task<MenuResponse> GetMenusAsync();
    }
}
