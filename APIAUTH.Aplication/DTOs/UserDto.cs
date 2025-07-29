using APIAUTH.Aplication.Helpers;
using APIAUTH.Domain.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APIAUTH.Aplication.DTOs
{
    public class UserDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }

        public int? Document { get; set; }

        public DocumentType? DocumentType { get; set; }

        public string? DocumentTypeValue => DocumentType != null ? DocumentType.ToString() : null;

        public string Phone { get; set; }
        public string? AvatarUrl { get; set; }
        public string Email { get; set; }
        public string BackupEmail { get; set; }

        public Gender? Gender { get; set; }
        public string GenderValue => Gender != null ? Gender.ToString() : null;



        public int? CompanyId { get; set; }
        public CompanyDto Company { get; set; }

        [JsonIgnore]
        public int AccountId { get; set; }
        [JsonIgnore]
        public AccountDto? Account { get; set; }

        public int RoleId   { get; set; }
        public RoleDto? Role { get; set; }

        public List<AddressDto> Addresses { get; set; }


    }
}
