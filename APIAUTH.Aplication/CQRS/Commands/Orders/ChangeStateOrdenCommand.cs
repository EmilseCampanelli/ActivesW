using APIAUTH.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Orders
{
    public class ChangeStateOrdenCommand : IRequest<Unit>
    {
        public int OrdenId { get; set; }
        public int newState { get; set; }
    }
}
