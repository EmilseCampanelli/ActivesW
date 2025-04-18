using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Producto.UpdateProducto
{
    public record UpdateProductoCommand(
        int Id,
        string Nombre,
        string Descripcion,
        double PrecioUnitatio,
        int Stock,
        int CategoriaId
    ) : IRequest<int>;
}
