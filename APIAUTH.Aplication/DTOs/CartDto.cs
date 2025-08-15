using APIAUTH.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.DTOs
{
    public class CartDto : BaseEntityDto
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public OrdenState OrdenState { get; set; }

        public List<ProductLineDto> ProductLine { get; set; }
    }
}
