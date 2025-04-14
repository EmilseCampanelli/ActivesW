using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.DTOs
{
    public class LoginRequest
    {
        public string Email { get; set; } //TODO: email
        public string Password { get; set; }
    }
}
