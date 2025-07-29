using APIAUTH.Domain.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace APIAUTH.Aplication.DTOs
{
    public class BaseEntityDto : BaseDto
    {
        public DateTime? UpdatedDate { get; set; }

        [JsonIgnore]
        public DateTime? CreatedDate { get; set; }
        public BaseState? State { get; set; }
    }
}
