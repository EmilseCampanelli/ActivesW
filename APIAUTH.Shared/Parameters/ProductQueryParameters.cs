namespace APIAUTH.Shared.Parameters
{
    public class ProductQueryParameters : QueryParameters
    {
        public int? CategoryId { get; set; }

        public int UserId { get; set; }

        public int? State {  get; set; }
    }
}
