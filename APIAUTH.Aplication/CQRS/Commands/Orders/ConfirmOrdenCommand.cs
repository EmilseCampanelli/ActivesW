using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Orders
{
    public class ConfirmOrdenCommand : IRequest<bool>
    {
        public int UserId { get; set; }
    }
}
