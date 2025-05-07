using APIAUTH.Domain.Enums;

namespace APIAUTH.Aplication.DTOs
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsGenericPassword { get; set; }
        public DateTime PasswordDate { get; set; }
        public BaseState BaseState { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryDate { get; set; }
    }
}
