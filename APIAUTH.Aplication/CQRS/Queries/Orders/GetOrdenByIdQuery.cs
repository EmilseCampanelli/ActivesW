using APIAUTH.Aplication.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Queries.Orders
{
    public class GetOrdenByIdQuery : IRequest<OrdenDto>
    {
        public int Id { get; set; }

        public GetOrdenByIdQuery(int id)
        {
            Id = id;
        }
    }
}
