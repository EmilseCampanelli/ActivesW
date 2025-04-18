using APIAUTH.Aplication.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> Exists(int id);
        Task<CuentaDto> Save(UsuarioDto dto);
        Task RecoverPassword(string email);
        Task<bool> ChangePassword(UserPasswordDto dto);
        Task<CuentaDto> ActivePassword(UsuarioDto? collaborator);
    }
}
