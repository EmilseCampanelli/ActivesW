using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Producto
{
    public class DeleteProductoCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteProductoCommand(int id)
        {
            Id = id;
        }
    }
}
