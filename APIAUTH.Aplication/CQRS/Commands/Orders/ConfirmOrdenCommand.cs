using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Orders
{
    public class ConfirmOrdenCommand : IRequest<bool>
    {
        [BindNever]
        public int UserId { get; set; }
    }
}
