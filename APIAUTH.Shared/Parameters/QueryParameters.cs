namespace APIAUTH.Shared.Parameters
{
    public class QueryParameters
    {
        public string? Search { get; set; }
        public string? FilterBy { get; set; }
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
        public string Order { get; set; } = "asc";
        public string OrderBy { get; set; } = "id";
    }
}
