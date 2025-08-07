using APIAUTH.Data.Filtered;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Enums;
using APIAUTH.Shared.Parameters;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace APIAUTH.Aplication.Filtered
{
    public class ProductEntityFilter : IEntityFilter<Product>
    {
        public IQueryable<Product> ApplyFilters(IQueryable<Product> query, QueryParameters parameters)
        {
            var p = parameters as ProductQueryParameters;

            if (p == null) return query;

            query = query.Where(x => x.ProductState == Domain.Enums.ProductState.Disponible);

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

                    var lambda = Expression.Lambda<Func<Product, bool>>(ilikeCall, parameter);
                    query = query.Where(lambda);

                }
            }

            if (p.CategoryId.HasValue)
                query = query.Where(x => x.CategoryId == p.CategoryId);

            if (p.State.HasValue && Enum.IsDefined(typeof(ProductState), p.State.Value))
            {
                var stateEnum = (ProductState)p.State.Value;
                query = query.Where(x => x.ProductState == stateEnum);
            }

            if (!p.State.HasValue)
            {
                query = query.Where(x => x.ProductState == ProductState.Disponible);
            }


            return query;
        }
    }


}
