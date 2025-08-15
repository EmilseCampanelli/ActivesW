using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.DTOs
{
    public class ComboSizeDto
    {
        public ComboSizeDto() { }
        public ComboSizeDto(string id, string descripcion)
        {
            Id = id;
            Description = descripcion;
        }
        public string Id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}
