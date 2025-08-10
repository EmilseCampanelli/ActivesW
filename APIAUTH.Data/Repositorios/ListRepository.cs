using APIAUTH.Data.Context;
using APIAUTH.Data.Filtered;
using APIAUTH.Domain.Repository;
using APIAUTH.Shared.Parameters;
using APIAUTH.Shared.Response;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Linq.Dynamic.Core;

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

                // total antes de paginar
                var total = await query.CountAsync();

                // Normalizar inputs
                var orderBy = parameters.OrderBy?.Trim();
                var dir = parameters.Order?.Equals("desc", StringComparison.OrdinalIgnoreCase) == true ? "desc" : "asc";
                var page = parameters.Page <= 0 ? 1 : parameters.Page;
                var limit = parameters.Limit <= 0 ? 10 : parameters.Limit;

                // Si no vino OrderBy, usar un fallback estable (Id si existe, o CreatedDate, etc.)
                if (string.IsNullOrWhiteSpace(orderBy))
                    orderBy = GetDefaultOrderBy<TEntity>() ?? "Id";

                // Validar que el path sea ordenable (escalar, no colección)
                if (!IsSortablePath<TEntity>(orderBy))
                    orderBy = GetDefaultOrderBy<TEntity>() ?? "Id";

                // Orden dinámico (soporta "Category.Description")
                query = query.OrderBy($"{orderBy} {dir}");

                var skip = (page - 1) * limit;

                // Proyectar en SQL y paginar
                var data =  query
                    .Select(selector)
                    .Skip(skip)
                    .Take(limit)
                    .ToList();

                return new PagedResponse<TDto>
                {
                    Total = total,
                    Page = page,
                    Order = dir,
                    OrderBy = orderBy,
                    Data = data,
                    TotalPages = (int)Math.Ceiling((double)total / limit)
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error real: " + ex);
                throw new ApplicationException("Error interno en paginación", ex);
            }
        } 

        private static string? GetDefaultOrderBy<T>()
        {
            var t = typeof(T);
            // Id, <TypeName>Id, CreatedDate…
            if (t.GetProperty("Id", BindingFlags.Public | BindingFlags.Instance) != null) return "Id";
            var alt = t.GetProperty($"{t.Name}Id", BindingFlags.Public | BindingFlags.Instance);
            if (alt != null) return alt.Name;
            if (t.GetProperty("CreatedDate", BindingFlags.Public | BindingFlags.Instance) != null) return "CreatedDate";
            return null;
        }

        // Valida que el path (p.ej. "Category.Description") termine en propiedad ESCALAR (no colección)
        private static bool IsSortablePath<T>(string path)
        {
            var type = typeof(T);
            foreach (var segment in path.Split('.'))
            {
                var pi = type.GetProperty(segment,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (pi == null) return false;

                // si es colección y no string -> no ordenable
                var pt = pi.PropertyType;
                var isEnumerable = typeof(System.Collections.IEnumerable).IsAssignableFrom(pt) && pt != typeof(string);
                if (isEnumerable) return false;

                type = Nullable.GetUnderlyingType(pt) ?? pt; // avanzar para anidadas
            }

            // última propiedad: debe ser escalar o string/enum/valor
            return type.IsValueType || type == typeof(string);
        }
    }


}
