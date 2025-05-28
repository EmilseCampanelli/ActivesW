using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Producto.ProductCart.Create
{
    public record CreateProductCartCommand(
        int UserId,
        int ProductId,
        int Quantity,
        string Size
        ) : IRequest<int>;
}
