namespace APIAUTH.Shared.Response
{
    public class PagedResponse<T>
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public string OrderBy { get; set; }
        public string Order { get; set; }
        public List<T> Data { get; set; } = new();
    }
}
