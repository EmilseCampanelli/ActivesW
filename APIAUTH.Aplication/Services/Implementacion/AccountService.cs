using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Repository;
using AutoMapper;

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

        //TODO:
        /*
         * Alerta de vencimiento de contraseña (DEJAR PARA EL FINAL) con el campo PasswordDate
         */

        public async Task RecoverPassword(string email)
        {
            var collaborator = _repository.GetByEmail(email);
            if (collaborator == null || collaborator.State == Domain.Enums.BaseState.Activo)
            {
                throw new UnauthorizedAccessException("Account is non-existent");
            }

            //TODO: Se debe bloquear al usuario y devolver el id del usuario administrador
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
                    throw new UnauthorizedAccessException("Incorrect Password");
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
