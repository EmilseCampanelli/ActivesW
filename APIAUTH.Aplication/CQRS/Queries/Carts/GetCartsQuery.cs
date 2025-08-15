using APIAUTH.Aplication.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Queries.Carts
{
    public class GetCartsQuery : IRequest<List<CartDto>>
    {
        public int UserId { get; set; }

        public GetCartsQuery(int userId)
        {
            UserId = userId;
        }

        public GetCartsQuery()
        {
            
        }
    }
}
