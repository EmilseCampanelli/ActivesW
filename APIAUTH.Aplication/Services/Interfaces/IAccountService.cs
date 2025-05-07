using APIAUTH.Aplication.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> Exists(int id);
        Task<AccountDto> Save(UserDto dto);
        Task RecoverPassword(string email);
        Task<bool> ChangePassword(UserPasswordDto dto);
        Task<AccountDto> ActivePassword(UserDto? collaborator);
    }
}
