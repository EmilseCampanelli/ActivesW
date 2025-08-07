using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace APIAUTH.Aplication.DTOs
{
    public class UserPasswordDto
    {
        [JsonIgnore]
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
