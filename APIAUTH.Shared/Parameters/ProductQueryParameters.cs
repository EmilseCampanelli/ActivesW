using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace APIAUTH.Shared.Parameters
{
    public class ProductQueryParameters : QueryParameters
    {
        public string? CategoryId { get; set; }

        [BindNever]
        public int UserId { get; set; }

        public int? State {  get; set; }

        public string? GenderId { get; set; }

        public string? SizeId { get; set; }
    }
}
