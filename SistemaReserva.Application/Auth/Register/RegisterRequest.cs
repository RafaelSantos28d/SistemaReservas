using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Application.Auth.Register
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

    }
}
