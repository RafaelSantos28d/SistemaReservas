using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Application.Auth.Login
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
