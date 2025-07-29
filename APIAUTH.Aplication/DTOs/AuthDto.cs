using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.DTOs
{
    public class AuthDto
    {
        public string IdToken { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public AuthDto(string idToken, string accessToken)
        {
            IdToken = idToken;
            AccessToken = accessToken;
        }
    }
}
