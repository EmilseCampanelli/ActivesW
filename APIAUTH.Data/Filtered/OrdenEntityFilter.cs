using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Enums;
using APIAUTH.Shared.Parameters;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
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

                    var parameter = Expression.Parameter(typeof(Orden), "x");
                    var propertyAccess = Expression.Property(parameter, property.Name);

                    if (property.PropertyType == typeof(string))
                    {
                        var efFunctions = Expression.Property(null, typeof(EF).GetProperty(nameof(EF.Functions))!);
                        var ilikeMethod = typeof(NpgsqlDbFunctionsExtensions)
                            .GetMethod(nameof(NpgsqlDbFunctionsExtensions.ILike), new[] { typeof(DbFunctions), typeof(string), typeof(string) })!;
                        var pattern = Expression.Constant($"%{p.Search}%");
                        var call = Expression.Call(ilikeMethod, efFunctions, propertyAccess, pattern);
                        var lambda = Expression.Lambda<Func<Orden, bool>>(call, parameter);
                        query = query.Where(lambda);
                    }
                    else if (IsEnumOrNullableEnum(property.PropertyType))
                    {
                        if (TryParseEnumFromNameOrDisplay(property.PropertyType, p.Search, out var enumValueObj))
                        {
                            var constant = Expression.Constant(enumValueObj, property.PropertyType);
                            var eq = Expression.Equal(propertyAccess, constant);
                            var lambda = Expression.Lambda<Func<Orden, bool>>(eq, parameter);
                            query = query.Where(lambda);
                        }
                        // si no parsea, no aplica filtro (o podrías forzar 0 resultados)
                    }
                    // NÚMEROS -> intentar parsear y comparar ==
                    else if (TryChangeType(p.Search, property.PropertyType, out var typedValue))
                    {
                        var constant = Expression.Constant(typedValue, property.PropertyType);
                        var eq = Expression.Equal(propertyAccess, constant);
                        var lambda = Expression.Lambda<Func<Orden, bool>>(eq, parameter);
                        query = query.Where(lambda);
                    }
                }
            }


            return query;
        }

        private static bool IsEnumOrNullableEnum(Type t)
     => t.IsEnum || (Nullable.GetUnderlyingType(t)?.IsEnum ?? false);

        // Admite: nombre del enum, Display(Name) y número
        private static bool TryParseEnumFromNameOrDisplay(Type typeOrNullable, string input, out object? value)
        {
            var enumType = Nullable.GetUnderlyingType(typeOrNullable) ?? typeOrNullable;
            foreach (var name in Enum.GetNames(enumType))
            {
                if (string.Equals(name, input, StringComparison.OrdinalIgnoreCase))
                {
                    value = Enum.Parse(enumType, name);
                    if (typeOrNullable != enumType) value = Activator.CreateInstance(typeOrNullable, value); // Nullable<>
                    return true;
                }
                var member = enumType.GetMember(name).First();
                var display = member.GetCustomAttribute<DisplayAttribute>()?.Name;
                if (!string.IsNullOrEmpty(display) &&
                    string.Equals(display, input, StringComparison.OrdinalIgnoreCase))
                {
                    value = Enum.Parse(enumType, name);
                    if (typeOrNullable != enumType) value = Activator.CreateInstance(typeOrNullable, value);
                    return true;
                }
            }
            // numérico
            if (int.TryParse(input, out var i))
            {
                var boxed = Enum.ToObject(enumType, i);
                value = (typeOrNullable != enumType) ? Activator.CreateInstance(typeOrNullable, boxed) : boxed;
                return true;
            }
            value = null;
            return false;
        }

        private static bool TryChangeType(string input, Type targetType, out object? value)
        {
            try
            {
                var nonNullType = Nullable.GetUnderlyingType(targetType) ?? targetType;
                value = Convert.ChangeType(input, nonNullType);
                if (nonNullType != targetType) // volver a Nullable<>
                    value = Activator.CreateInstance(targetType, value);
                return true;
            }
            catch
            {
                value = null;
                return false;
            }
        }
    }
}
