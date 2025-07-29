using APIAUTH.Aplication.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthDto> AuthenticateUserAsync(string email, string password);
        Task<AuthDto> AuthenticateWithGoogleAsync(string idTokenGoogle);
        Task<AuthDto> RefreshTokensAsync(string refreshToken);
    }
}
