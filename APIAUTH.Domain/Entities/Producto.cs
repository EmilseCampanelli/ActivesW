using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Entities
{
    public class Producto : BaseEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double PrecioUnitario { get; set; }
        public int Stock {  get; set; }
        public string ImagesUrl { get; set; }
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
        public string Slug { get; set; }
    }
}
