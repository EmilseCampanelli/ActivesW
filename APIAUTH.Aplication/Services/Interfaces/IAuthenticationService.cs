﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<(string idToken, string accessToken, string refreshToken)> AuthenticateUserAsync(string email, string password);
        Task<(string idToken, string accessToken, string refreshToken)> AuthenticateWithGoogleAsync(string idTokenGoogle);
        Task<(string idToken, string accessToken, string refreshToken)> RefreshTokensAsync(string refreshToken);
    }
}
