using APIAUTH.Domain.Entities;
using APIAUTH.Shared.Parameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Data.Filtered
{
    public class UserEntityFilter : IEntityFilter<User>
    {

        public IQueryable<User> ApplyFilters(IQueryable<User> query, QueryParameters parameters)
        {
            var p = parameters as UserQueryParameters;

            if (p == null) return query;

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

                    var lambda = Expression.Lambda<Func<User, bool>>(ilikeCall, parameter);
                    query = query.Where(lambda);
                }
            }

            return query;
        }
    }
}
