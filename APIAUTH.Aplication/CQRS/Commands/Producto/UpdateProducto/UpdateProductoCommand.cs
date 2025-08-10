using APIAUTH.Aplication.DTOs;
using APIAUTH.Domain.Enums;
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
        string Title,
        string Description,
        double Price,
        int Stock,
        int CategoryId,
        string[] Sizes,
        string[] Tags,
        Gender Gender,
        List<ProductImageAddDto> ProductImage
    ) : IRequest<int>;
}
