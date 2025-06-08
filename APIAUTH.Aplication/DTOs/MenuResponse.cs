namespace APIAUTH.Aplication.DTOs
{

    public class MenuResponse
    {
        public string Title { get; set; }
        public List<MenuItemDto> Menu { get; set; }
    }

    public class MenuItemDto
    {
        public string Label { get; set; }
        public string Path { get; set; }
        public string Icon { get; set; }
        public List<MenuChildDto> Children { get; set; }
    }

    public class MenuChildDto
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Path { get; set; }
    }
}
