using APIAUTH.Shared.Parameters;
using APIAUTH.Shared.Response;

namespace APIAUTH.Domain.Repository
{
    public interface IListRepository<TEntity>
    {
        Task<PagedResponse<TDto>> GetPagedResultAsync<TDto>(
             IQueryable<TEntity> query,
             QueryParameters parameters,
             Func<TEntity, TDto> selector);
    }
}
