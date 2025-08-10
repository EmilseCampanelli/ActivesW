using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.DTOs
{
    public class ProductImageAddDto
    {
        public string Image { get; set; }
        public int Orden {  get; set; }
        public string? Name { get; set; }
    }

    public class ProductImageDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public int Orden { get; set; }
    }
}
