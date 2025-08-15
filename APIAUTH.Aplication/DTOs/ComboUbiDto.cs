using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.DTOs
{
    public class ComboUbiDto
    {
        public ComboUbiDto() { }
        public ComboUbiDto(string id, string descripcion)
        {
            Id = id;
            Description = descripcion;
        }
        public string Id { get; set; }
        public string Description { get; set; }
    }
}
