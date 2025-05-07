using APIAUTH.Domain.Enums;

namespace APIAUTH.Aplication.DTOs
{
    public class UserGetDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Document { get; set; }
        public DocumentType DocumentType { get; set; }
        public string Phone { get; set; }
        public string? AvatarUrl { get; set; }
        public string Email { get; set; }
        public string BackupEmail { get; set; }
        public Gender Gender { get; set; }
        public RoleDto? Role { get; set; }
        public List<AddressDto> Addresses { get; set; }
    }
}
