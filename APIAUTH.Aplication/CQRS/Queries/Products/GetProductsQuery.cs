using APIAUTH.Aplication.DTOs;
using APIAUTH.Shared.Parameters;
using APIAUTH.Shared.Response;
using MediatR;

namespace APIAUTH.Aplication.CQRS.Queries.Products
{
    public class GetProductsQuery : IRequest<PagedResponse<ProductDto>>
    {
        public ProductQueryParameters Parameters { get; set; }

        public int UserId { get; set; }

        public GetProductsQuery(ProductQueryParameters parameters)
        {
            Parameters = parameters;
            UserId = parameters.UserId;
        }
    }
}
