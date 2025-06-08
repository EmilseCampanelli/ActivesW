using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Domain.Repository;

namespace APIAUTH.Aplication.Services.Implementacion
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;

        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<MenuResponse> GetMenusAsync()
        {
            var menus = await _menuRepository.GetMenusAsync();

            return new MenuResponse
            {
                Menu = menus.Select(menu => new MenuItemDto
                {
                    Label = menu.Label,
                    Path = menu.Path,
                    Icon = menu.Icon,
                    Children = menu.Children.Select(child => new MenuChildDto
                    {
                        Id = child.Id,
                        Label = child.Label,
                        Path = child.Path
                    }).ToList()
                }).ToList()
            };
        }
    }
}
