using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using APIAUTH.Aplication.DTOs;
using APIAUTH.Domain.Enums;
using APIAUTH.Aplication.Services.Interfaces;

namespace APIAUTH.Aplication.Services.Implementacion
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<(string idToken, string accessToken, string refreshToken)> AuthenticateUserAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            var usuarios = _userRepository.GetCollaboratorByIdUser(user.Id);
            if (user == null)
            {
                throw new UnauthorizedAccessException("El usuario con el que desea ingresar no existe.");
            }
            if (!await _userRepository.ValidatePasswordAsync(user, password))
            {
                throw new UnauthorizedAccessException("La contraseña con la que desea ingresar es incorrecta.");
            }
            if (usuarios.State != BaseState.Activo)
            {
                throw new UnauthorizedAccessException("El usuario no se encuentra activo en este momento.");
            }

            var idToken = GenerateIdToken(usuarios);
            var accessToken = GenerateAccessToken(usuarios);
            var refreshToken = GenerateRefreshToken();

            SaveRefreshTokenAsync(user.Id, refreshToken);

            return (idToken, accessToken, refreshToken);
        }

        private string GenerateIdToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Cuenta.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim("idUser", usuario.Id.ToString()),
                new Claim("Descripcion", $"{usuario.Apellido}, {usuario.Nombre}"),
                new Claim("email", usuario.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:IdTokenLifetimeMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateAccessToken(Usuario usuario)
        {

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Cuenta.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, usuario.Rol.Descripcion),
                new Claim("isGenericPassword", usuario.Cuenta.IsGenericPassword.ToString()),

            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:AccessTokenLifetimeMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<(string idToken, string accessToken, string refreshToken)> AuthenticateWithGoogleAsync(string idTokenGoogle)
        {

            string email = GetEmailFromIdToken(idTokenGoogle);

            var collaborator = _userRepository.GetByEmail(email);

            if (collaborator == null)
            {
                throw new UnauthorizedAccessException("El usuario con el que desea ingresar no existe.");
            }
            if (collaborator.State != BaseState.Activo)
            {
                throw new UnauthorizedAccessException("El usuario no se encuentra activo en este momento.");
            }

            var idToken = GenerateIdToken(collaborator);
            var accessToken = GenerateAccessToken(collaborator);
            var refreshToken = GenerateRefreshToken();

            SaveRefreshTokenAsync(collaborator.CuentaId, refreshToken);

            return (idToken, accessToken, refreshToken);
        }

        private string GetEmailFromIdToken(string idToken)
        {
            // Inicializa el manejador de JWT
            var handler = new JwtSecurityTokenHandler();

            // Lee el token para extraer los datos
            var jwtToken = handler.ReadJwtToken(idToken);

            // Accede a los claims del payload
            var emailClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "email");

            // Retorna el valor del email si existe
            return emailClaim?.Value ?? "No se encontró el email en el id_token";
        }

        private async Task<GoogleTokenResponse> GetAccessByGoogle(string authorizationCode)
        {

            var responseData = new GoogleTokenResponse();

            return responseData;
        }

        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }

        public async Task<(string idToken, string accessToken, string refreshToken)> RefreshTokensAsync(string refreshToken)
        {
            var user = await _userRepository.GetUserByRefreshTokenAsync(refreshToken);
            var collaborator = _userRepository.GetCollaboratorByIdUser(user.Id);


            if (user == null || user.BaseState != BaseState.Activo)
            {
                throw new UnauthorizedAccessException("Invalid refresh token.");
            }

            if (user.RefreshTokenExpiryDate < DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Refresh token has expired.");
            }

            var idToken = GenerateIdToken(collaborator);
            var accessToken = GenerateAccessToken(collaborator);

            var newRefreshToken = GenerateRefreshToken();
            SaveRefreshTokenAsync(user.Id, newRefreshToken);

            return (idToken, accessToken, refreshToken);
        }

        public async void SaveRefreshTokenAsync(int userId, string refreshToken)
        {
            var user = await _userRepository.Get(userId);
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryDate = DateTime.UtcNow.AddDays(30);

            await _userRepository.Update(user);
        }

    }
}
