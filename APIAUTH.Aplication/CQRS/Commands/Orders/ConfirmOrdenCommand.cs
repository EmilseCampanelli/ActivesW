using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Orders
{
    public class ConfirmOrdenCommand : IRequest<string>
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public decimal CostTracking {  get; set; }
    }
}
