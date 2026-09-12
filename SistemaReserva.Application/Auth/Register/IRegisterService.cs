using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Application.Auth.Register
{
    public interface IRegisterService
    {
        Task Register(RegisterRequest request);
    }
}
