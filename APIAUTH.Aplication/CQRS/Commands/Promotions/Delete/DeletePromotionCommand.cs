using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Promotions.Delete
{
    public record DeletePromotionCommand(int Id) : IRequest<Unit>;
}
