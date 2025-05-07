namespace APIAUTH.Aplication.DTOs
{
    public class AddressDto : BaseEntityDto
    {
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Ciudad { get; set; }
        public int ProvinciaId { get; set; }
        public string ProvinciaName { get; set; }
        public int PaisId { get; set; }
        public string PaisName { get; set; }
        public string CodigoPostal { get; set; }
        public int UsuarioId { get; set; }

    }
}
