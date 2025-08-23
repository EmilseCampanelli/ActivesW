using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Repository;
using AutoMapper;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace APIAUTH.Aplication.Services.Implementacion
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public AccountService(IMapper mapper, IUserRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Exists(int id)
        {
            return await _repository.Get(id) != null;
        }

        public async Task<AccountDto> Save(UserDto dto)
        {
            var user = new Account();

            var collaborator = _repository.GetByEmail(dto.Email);
            if (collaborator != null)
            {
                throw new UnauthorizedAccessException("El usuario ya posee una cuenta con este email");
            }

            try
            {
                user.Email = dto.Email;
                user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Account.Password);
                user.IsGenericPassword = false;
                user.PasswordDate = DateTime.UtcNow;
                user.BaseState = Domain.Enums.BaseState.Activo;
            }
            catch (FormatException ex)
            {
                throw new Exception(ex.Message);
            }

            return _mapper.Map<AccountDto>(user);
        }


        public async Task RecoverPassword(string email)
        {
            var collaborator = _repository.GetByEmail(email);
            if (collaborator == null || collaborator.Status != Domain.Enums.BaseState.Activo)
            {
                throw new UnauthorizedAccessException("Usuario inexistente");
            }

            var temporalPassword = GenerateSecurePassword(10);
            var user = collaborator.Account;
            user.Password = BCrypt.Net.BCrypt.HashPassword(temporalPassword);
            user.PasswordDate = DateTime.UtcNow;
            user.IsGenericPassword = true;
            await _repository.Update(user);

            await SendEmailAsync(collaborator, temporalPassword);

        }

        private static string GenerateSecurePassword(int length)
        {
            const string valid = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz23456789!@#$%&*";
            var bytes = new byte[length];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);

            var sb = new StringBuilder(length);
            foreach (var b in bytes)
                sb.Append(valid[b % valid.Length]);

            return sb.ToString();
        }

        private async Task SendEmailAsync(User to, string tempPassword)
        {
            var from = "no-reply@ActiveSW.com";
            var subject = "Recuperación de contraseña";
            var body = $@"
                Hola {to.Name},

                Tu contraseña temporal es: {tempPassword}

                Por seguridad, deberás cambiarla al iniciar sesión.";

            using var client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("emicampanelli16@gmail.com", "xlky qkzv euwm bfhn"),
                EnableSsl = true
            };

            using var message = new MailMessage(from, to.Email, subject, body);

            await client.SendMailAsync(message);
        }


        public async Task<bool> ChangePassword(UserPasswordDto dto)
        {
            var collaborator = _repository.GetByEmail(dto.Email);
            if (collaborator != null && collaborator.Account != null)
            {
                if (await _repository.ValidatePasswordAsync(collaborator.Account, dto.CurrentPassword))
                {
                    var user = collaborator.Account;
                    user.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
                    user.PasswordDate = DateTime.UtcNow;
                    user.IsGenericPassword = false;
                    return await _repository.Update(user) != null;
                }
                else
                {
                    throw new UnauthorizedAccessException("La contraseña actual ingresada es incorrecta");
                }
            }

            throw new UnauthorizedAccessException("Account is non-existent");
        }

        public async Task<AccountDto> ActivePassword(UserDto? collaborator)
        {
            var user = collaborator.Account;
            user.Password = BCrypt.Net.BCrypt.HashPassword(collaborator.Document.ToString());
            user.IsGenericPassword = true;
            user.PasswordDate = DateTime.UtcNow;

            return user;
        }
    }
}
