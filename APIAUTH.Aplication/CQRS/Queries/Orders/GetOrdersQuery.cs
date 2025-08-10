using APIAUTH.Aplication.DTOs;
using APIAUTH.Shared.Parameters;
using APIAUTH.Shared.Response;
using MediatR;

namespace APIAUTH.Aplication.CQRS.Queries.Orders
{
    public class GetOrdersQuery : IRequest<PagedResponse<OrdenDto>>
    {
        public OrdenQueryParameters Parameters { get; set; }

        public int UserId { get; set; }

        public GetOrdersQuery(OrdenQueryParameters parameters)
        {
            UserId = parameters.UserId;
            Parameters = parameters;
        }
    }
}
