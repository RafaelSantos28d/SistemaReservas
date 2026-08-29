using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Application.Auth.Register
{
    public interface IRegisterService
    {
        Task Register(RegisterRequest request);
    }
}
