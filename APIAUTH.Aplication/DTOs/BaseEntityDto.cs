using APIAUTH.Domain.Enums;

namespace APIAUTH.Aplication.DTOs
{
    public class BaseEntityDto : BaseDto
    {
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public BaseState? State { get; set; }
    }
}
