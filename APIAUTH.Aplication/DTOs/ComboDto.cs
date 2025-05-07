namespace APIAUTH.Aplication.DTOs
{
    public class ComboDto
    {
        public ComboDto() { }
        public ComboDto(int id, string descripcion)
        {
            Id = id;
            Description = descripcion;
        }
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
