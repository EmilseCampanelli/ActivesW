using APIAUTH.Data.Filtered;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Enums;
using APIAUTH.Shared.Parameters;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;

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
                var entityType = typeof(Product);
                var property = entityType.GetProperty(p.FilterBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property != null)
                {
                    var parameter = Expression.Parameter(typeof(Product), "x");
                    var propertyAccess = Expression.Property(parameter, property.Name);

                    if (property.PropertyType == typeof(string))
                    {
                        var efFunctions = Expression.Property(null, typeof(EF).GetProperty(nameof(EF.Functions))!);
                        var ilikeMethod = typeof(NpgsqlDbFunctionsExtensions)
                            .GetMethod(nameof(NpgsqlDbFunctionsExtensions.ILike), new[] { typeof(DbFunctions), typeof(string), typeof(string) })!;
                        var pattern = Expression.Constant($"%{p.Search}%");
                        var call = Expression.Call(ilikeMethod, efFunctions, propertyAccess, pattern);
                        var lambda = Expression.Lambda<Func<Product, bool>>(call, parameter);
                        query = query.Where(lambda);
                    }
                    else if (IsEnumOrNullableEnum(property.PropertyType))
                    {
                        if (TryParseEnumFromNameOrDisplay(property.PropertyType, p.Search, out var enumValueObj))
                        {
                            var constant = Expression.Constant(enumValueObj, property.PropertyType);
                            var eq = Expression.Equal(propertyAccess, constant);
                            var lambda = Expression.Lambda<Func<Product, bool>>(eq, parameter);
                            query = query.Where(lambda);
                        }
                        // si no parsea, no aplica filtro (o podrías forzar 0 resultados)
                    }
                    // NÚMEROS -> intentar parsear y comparar ==
                    else if (TryChangeType(p.Search, property.PropertyType, out var typedValue))
                    {
                        var constant = Expression.Constant(typedValue, property.PropertyType);
                        var eq = Expression.Equal(propertyAccess, constant);
                        var lambda = Expression.Lambda<Func<Product, bool>>(eq, parameter);
                        query = query.Where(lambda);
                    }

                }
            }

            if (!string.IsNullOrWhiteSpace(p.CategoryId))
            {
                var categoryIds = p.CategoryId
                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(id => int.Parse(id))
                    .ToList();

                query = query.Where(x => categoryIds.Contains(x.CategoryId));
            }

            if (p.State.HasValue && Enum.IsDefined(typeof(ProductState), p.State.Value))
            {
                var stateEnum = (ProductState)p.State.Value;
                query = query.Where(x => x.ProductState == stateEnum);
            }

            if (!p.State.HasValue)
            {
                query = query.Where(x => x.ProductState == ProductState.Disponible);
            }
          

            if (!string.IsNullOrWhiteSpace(p.GenderId))
            {
                var genderIds = p.GenderId
                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();

                query = query.Where(x => genderIds.Contains((int)x.Gender));
            }



            if (!string.IsNullOrWhiteSpace(p.SizeId))
            {
                var sizeIds = p.SizeId
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();

                var selectedSizes = sizeIds
                    .Where(id => Enum.IsDefined(typeof(Sizes), id))
                    .Select(id => Enum.GetName(typeof(Sizes), id))
                    .ToList();

                query = query.Where(x =>
                    x.Sizes != null &&
                    x.Sizes
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Any(s => selectedSizes.Contains(s, StringComparer.OrdinalIgnoreCase))
                );
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
