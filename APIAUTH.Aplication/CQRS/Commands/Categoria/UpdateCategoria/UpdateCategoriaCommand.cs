using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Categoria.UpdateCategoria
{
    public record UpdateCategoriaCommand(
        int Id,
        string Descripcion
    ) : IRequest<int>;
}
