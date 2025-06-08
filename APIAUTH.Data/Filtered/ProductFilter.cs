using APIAUTH.Data.Filtered;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Enums;
using APIAUTH.Shared.Parameters;
using Microsoft.EntityFrameworkCore;

namespace APIAUTH.Aplication.Filtered
{
    public class ProductEntityFilter : IEntityFilter<Product>
    {
        public IQueryable<Product> ApplyFilters(IQueryable<Product> query, QueryParameters parameters)
        {
            var p = parameters as ProductQueryParameters;

            if (p == null) return query;

            query = query.Where(x => x.ProductState == Domain.Enums.ProductState.Disponible);

            if (!string.IsNullOrEmpty(p.Search))
                query = query.Where(x => EF.Functions.ILike(x.Description, $"%{p.Search}%"));

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
