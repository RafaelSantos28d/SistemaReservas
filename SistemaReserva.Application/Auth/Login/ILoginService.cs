using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Application.Auth.Login
{
    public interface ILoginService
    {
        Task<LoginResponse> ExecuteAsync(LoginRequest request);
    }
}
