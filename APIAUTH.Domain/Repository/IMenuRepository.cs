using APIAUTH.Domain.Entities;

namespace APIAUTH.Domain.Repository
{
    public interface IMenuRepository
    {
        Task<List<Menu>> GetMenusAsync();
    }


}
