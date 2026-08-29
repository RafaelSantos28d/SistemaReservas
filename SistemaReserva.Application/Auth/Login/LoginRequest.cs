using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace SistemReserva.Application.Auth.Login
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
