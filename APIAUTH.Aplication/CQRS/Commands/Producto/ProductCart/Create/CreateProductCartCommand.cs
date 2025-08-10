using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Producto.ProductCart.Create
{
    public record CreateProductCartCommand : IRequest<int>
    {
        [JsonIgnore]
        public int UserId { get; set; } 
        public int ProductId { get; init; }
        public int Quantity { get; init; }
        public string Size { get; init; }
    }
}
