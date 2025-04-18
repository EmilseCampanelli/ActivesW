using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Producto.CreateProducto
{
    public class CreateProductoValidator : AbstractValidator<CreateProductoCommand>
    {
        public CreateProductoValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty();
            RuleFor(x => x.PrecioUnitatio).GreaterThan(0);
            RuleFor(x => x.CategoriaId).NotEmpty();
        }
    }
}
