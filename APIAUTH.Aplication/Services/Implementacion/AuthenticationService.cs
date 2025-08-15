using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Enums;
using APIAUTH.Domain.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIAUTH.Aplication.Services.Implementacion
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<User> _repository;
        private readonly IConfiguration _configuration;
        private readonly IRepository<Role> _roleRepository;

        public AuthenticationService(IUserRepository userRepository, IConfiguration configuration, IRepository<Role> roleRepository, IRepository<User> repository)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _roleRepository = roleRepository;
            _repository = repository;
        }

        public async Task<AuthDto> AuthenticateUserAsync(string email, string password)
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
            if (usuarios.Status != BaseState.Activo)
            {
                throw new UnauthorizedAccessException("El usuario no se encuentra activo en este momento.");
            }

            var idToken = GenerateIdToken(usuarios);
            //var accessToken = GenerateAccessToken(usuarios);
            var refreshToken = GenerateRefreshToken();

            SaveRefreshTokenAsync(user.Id, refreshToken);

            return new AuthDto(idToken);
        }

        private string GenerateIdToken(User usuario)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Account.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim("idUser", usuario.Id.ToString()),
                new Claim("Description", $"{usuario.LastName}, {usuario.Name}"),
                new Claim("email", usuario.Email),
                new Claim("role", usuario.Role.Description),
                new Claim("isGenericPassword", usuario.Account.IsGenericPassword.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:IdTokenLifetimeMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateAccessToken(User usuario)
        {

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Account.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, usuario.Role.Description),
                new Claim("isGenericPassword", usuario.Account.IsGenericPassword.ToString()),
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:AccessTokenLifetimeMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<AuthDto> AuthenticateWithGoogleAsync(string idTokenGoogle)
        {

            string email = GetEmailFromIdToken(idTokenGoogle);
            string fullName = GetFullNameFromIdToken(idTokenGoogle);

            var collaborator = _userRepository.GetByEmail(email);

            if (collaborator == null)
            {
                var customerRole = _roleRepository.GetFiltered(p => p.Description == "Customer").FirstOrDefault();
                if (customerRole == null)
                {
                    throw new Exception("No se encontró el rol 'Customer' en la base de datos.");
                }

                collaborator = new User
                {
                    Name = fullName,
                    Email = email,
                    RoleId = customerRole.Id,
                    Status = BaseState.Activo
                };

                await _repository.Add(collaborator);

                var idToken = GenerateIdToken(collaborator);


            }
            if (collaborator.Status != BaseState.Activo)
            {
                throw new UnauthorizedAccessException("El usuario no se encuentra activo en este momento.");
            }


            //var accessToken = GenerateAccessToken(collaborator);
            var refreshToken = GenerateRefreshToken();

            SaveRefreshTokenAsync(collaborator.Id, refreshToken);
            return new AuthDto(idTokenGoogle);
        }

        private string GetEmailFromIdToken(string idToken)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwtToken = handler.ReadJwtToken(idToken);

            var emailClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "email");

            return emailClaim?.Value ?? "No se encontró el email en el id_token";
        }

        private string GetFullNameFromIdToken(string idToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(idToken);

            var nameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "name")?.Value;

            if (string.IsNullOrEmpty(nameClaim))
            {
                var givenName = jwtToken.Claims.FirstOrDefault(c => c.Type == "given_name")?.Value;
                var familyName = jwtToken.Claims.FirstOrDefault(c => c.Type == "family_name")?.Value;
                nameClaim = $"{givenName} {familyName}".Trim();
            }

            return nameClaim ?? "Unknown User";
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

        public async Task<AuthDto> RefreshTokensAsync(string refreshToken)
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
           // var accessToken = GenerateAccessToken(collaborator);

            var newRefreshToken = GenerateRefreshToken();
            SaveRefreshTokenAsync(user.Id, newRefreshToken);

            return new AuthDto(idToken);
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
