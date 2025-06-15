using APIAUTH.Data.Context;
using APIAUTH.Data.Filtered;
using APIAUTH.Domain.Repository;
using APIAUTH.Shared.Parameters;
using APIAUTH.Shared.Response;
using Microsoft.EntityFrameworkCore;

namespace APIAUTH.Data.Repositorios
{
    public class ListRepository<TEntity> : IListRepository<TEntity>
    where TEntity : class
    {
        private readonly ActivesWContext _context;
        private readonly IEntityFilter<TEntity> _filter;

        public ListRepository(ActivesWContext context, IEntityFilter<TEntity> filter)
        {
            _context = context;
            _filter = filter;
        }

        public async Task<PagedResponse<TDto>> GetPagedResultAsync<TDto>(
      IQueryable<TEntity> query,
      QueryParameters parameters,
      Func<TEntity, TDto> selector)
        {
            try
            {
                query = _filter.ApplyFilters(query, parameters);

                var total = await query.CountAsync();

                var entityType = typeof(TEntity);
                var property = entityType.GetProperties()
                    .FirstOrDefault(p => string.Equals(p.Name, parameters.OrderBy, StringComparison.OrdinalIgnoreCase));

                if (property == null)
                    throw new ArgumentException($"La propiedad '{parameters.OrderBy}' no existe en la entidad '{entityType.Name}'.");

                // Usamos el nombre correcto de la propiedad (con casing válido)
                var validOrderBy = property.Name;


                query = parameters.Order.ToLower() == "desc"
                    ? query.OrderByDescending(x => EF.Property<object>(x, validOrderBy))
                    : query.OrderBy(x => EF.Property<object>(x, validOrderBy));


                var skip = (parameters.Page - 1) * parameters.Limit;

                var data = await query.Skip(skip).Take(parameters.Limit).ToListAsync();

                return new PagedResponse<TDto>
                {
                    Total = total,
                    Page = parameters.Page,
                    Order = parameters.Order,
                    OrderBy = parameters.OrderBy,
                    Data = data.Select(selector).ToList()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error real: " + ex.Message);
                throw new ApplicationException("Error interno en paginación", ex);
            }
        }

    }

}
