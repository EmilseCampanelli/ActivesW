using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Enums;
using APIAUTH.Shared.Parameters;
using Microsoft.EntityFrameworkCore;

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
                query = query.Where(x =>
                    EF.Functions.ILike(x.OrdenState.ToString(), $"%{p.Search}%"));
            }


            return query;
        }
    }
}
