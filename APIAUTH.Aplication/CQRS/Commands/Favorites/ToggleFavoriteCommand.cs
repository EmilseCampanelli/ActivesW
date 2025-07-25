using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Favorites
{
    public class ToggleFavoriteCommand : IRequest<Unit>
    {
        [BindNever]
        public int UserId { get; set; }

        public int ProductId { get; set; }
    }
}
