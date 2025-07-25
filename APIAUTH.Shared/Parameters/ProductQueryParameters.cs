using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace APIAUTH.Shared.Parameters
{
    public class ProductQueryParameters : QueryParameters
    {
        public int? CategoryId { get; set; }

        [BindNever]
        public int UserId { get; set; }

        public int? State {  get; set; }
    }
}
