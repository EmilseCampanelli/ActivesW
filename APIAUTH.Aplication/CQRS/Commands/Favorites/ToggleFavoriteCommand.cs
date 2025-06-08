using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Favorites
{
    public class ToggleFavoriteCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
