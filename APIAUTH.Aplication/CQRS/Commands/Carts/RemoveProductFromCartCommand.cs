using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Carts
{
    public class RemoveProductFromCartCommand : IRequest<Unit>
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}
