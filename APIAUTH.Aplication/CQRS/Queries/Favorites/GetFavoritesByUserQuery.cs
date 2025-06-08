using APIAUTH.Aplication.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Queries.Favorites
{
    public class GetFavoritesByUserQuery : IRequest<List<ProductDto>>
    {
        public int UserId { get; set; }

        public GetFavoritesByUserQuery(int userId)
        {
            UserId = userId;
        }
    }
}
