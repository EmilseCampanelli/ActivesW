using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Categoria.CreateCategoria
{
    public record CreateCategoriaCommand(
        string Descripcion
    ): IRequest<int>;
}
