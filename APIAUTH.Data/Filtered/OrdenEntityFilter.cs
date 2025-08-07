using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Enums;
using APIAUTH.Shared.Parameters;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace APIAUTH.Data.Filtered
{
    public class OrdenEntityFilter : IEntityFilter<Orden>
    {
        public IQueryable<Orden> ApplyFilters(IQueryable<Orden> query, QueryParameters parameters)
        {
            var p = parameters as UserQueryParameters;

            if (p == null) return query;

            if (int.TryParse(p.Search, out var searchValue))
            {
                var estado = (OrdenState)searchValue;
                query = query.Where(x => x.OrdenState == estado);
            }
            else
            {
                if (!string.IsNullOrEmpty(p.Search) && !string.IsNullOrEmpty(p.FilterBy))
                {
                    var entityType = typeof(User);
                    var property = entityType.GetProperty(p.FilterBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    if (property != null)
                    {
                        var parameter = Expression.Parameter(typeof(User), "x");
                        var propertyAccess = Expression.Property(parameter, property.Name);

                        // Llamada a EF.Functions.ILike(x.Propiedad, "%valor%")
                        var efFunctions = Expression.Property(null, typeof(EF).GetProperty(nameof(EF.Functions))!);
                        var ilikeMethod = typeof(NpgsqlDbFunctionsExtensions)
                            .GetMethod(nameof(NpgsqlDbFunctionsExtensions.ILike), new[] { typeof(DbFunctions), typeof(string), typeof(string) });

                        // Convertir propertyAccess a string si no lo es
                        Expression propertyAsString = property.PropertyType == typeof(string)
                            ? propertyAccess
                            : Expression.Call(propertyAccess, "ToString", null);

                        var searchPattern = Expression.Constant($"%{p.Search}%");
                        var ilikeCall = Expression.Call(ilikeMethod!, efFunctions, propertyAsString, searchPattern);

                        var lambda = Expression.Lambda<Func<Orden, bool>>(ilikeCall, parameter);
                        query = query.Where(lambda);

                    }
                }
            }


            return query;
        }
    }
}
