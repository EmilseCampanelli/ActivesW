using APIAUTH.Domain.Enums;

namespace APIAUTH.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Document { get; set; }
        public DocumentType TypeDocument { get; set; }
        public string Phone { get; set; }
        public string? AvatarUrl { get; set; }
        public string Email { get; set; }
        public string BackupEmail { get; set; }
        public Gender Gender { get; set; }

        public virtual List<Address>  Address {get; set;}

        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public int AccountId { get; set; }
        public virtual Account Account { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

    }
}
