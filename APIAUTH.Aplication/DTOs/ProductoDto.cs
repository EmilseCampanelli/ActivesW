using APIAUTH.Domain.Enums;

namespace APIAUTH.Aplication.DTOs
{
    public class ProductoDto : BaseEntityDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double PrecioUnitario { get; set; }
        public int Stock { get; set; }
        public string ImagesUrl { get; set; }
        public int CategoriaId { get; set; }
        public CategoriaDto Categoria { get; set; }
        public string Slug { get; set; }
        public ProductState EstadoProducto { get; set; }
    }
}
