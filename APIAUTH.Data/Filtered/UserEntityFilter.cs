using APIAUTH.Domain.Entities;
using APIAUTH.Shared.Parameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

            if (!string.IsNullOrEmpty(p.Search))
                query = query.Where(x => EF.Functions.ILike(x.Name, $"%{p.Search}%"));

            return query;
        }
    }
}
