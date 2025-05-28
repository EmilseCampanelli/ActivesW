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
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.CategoryId).NotEmpty();
        }
    }
}
