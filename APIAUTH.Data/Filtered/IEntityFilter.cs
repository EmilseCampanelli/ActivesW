using APIAUTH.Shared.Parameters;

namespace APIAUTH.Data.Filtered
{
    public interface IEntityFilter<TEntity>
    {
        IQueryable<TEntity> ApplyFilters(IQueryable<TEntity> query, QueryParameters parameters);
    }
}
