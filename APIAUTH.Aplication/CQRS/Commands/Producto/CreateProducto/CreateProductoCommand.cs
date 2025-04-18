using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Producto.CreateProducto
{
    public record CreateProductoCommand(
        string Nombre,
        string Descripcion,
        double PrecioUnitatio,
        int Stock,
        int CategoriaId
    ) : IRequest<int>;
}
