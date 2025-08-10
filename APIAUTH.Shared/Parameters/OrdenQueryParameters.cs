using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace APIAUTH.Shared.Parameters
{
    public class OrdenQueryParameters : QueryParameters
    {
        [BindNever]
        public int UserId { get; set; }
    }
}
