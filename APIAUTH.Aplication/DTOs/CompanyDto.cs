namespace APIAUTH.Aplication.DTOs
{
    public class CompanyDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Descripcion { get; set; }
        public string CUIT { get; set; }
        public string Direccion { get; set; }
        public DateTime OperationDate { get; set; }
    }
}
