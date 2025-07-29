using APIAUTH.Domain.Enums;
using MediatR;

namespace APIAUTH.Aplication.CQRS.Commands.Producto.CreateProducto
{
    public record CreateProductoCommand(
        string Title,
        string Description,
        double Price,
        int Stock,
        int CategoryId,
        string[] Sizes,
        string[] Tags,
        Gender Gender,
        List<string> Images
    ) : IRequest<int>;
}
